using BlogApp.Auth.Application;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure;
using BlogApp.Auth.Infrastructure.Persistance;
using BlogApp.Auth.Presentation.Middleware;
using BlogApp.Auth.Presentation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "BlogApp.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

builder.Services.AddIdentity<AppUser, AppRole>(config =>
{
    config.Password.RequiredLength = 6;
    config.Password.RequireDigit = true;
    config.Password.RequireLowercase = true;
    config.Password.RequireUppercase = true;
    config.Password.RequireNonAlphanumeric = false;
})
                .AddEntityFrameworkStores<BlogAuthDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<AppUser, AppRole, BlogAuthDbContext, Guid>>()
                .AddRoleStore<RoleStore<AppRole, BlogAuthDbContext, Guid>>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

builder.Services.AddControllersWithViews();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(config =>
{
    config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    var provider = builder.Services.BuildServiceProvider();
    var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (ApiVersionDescription description in service.ApiVersionDescriptions)
    {
        config.SwaggerDoc(description.GroupName,
                          new OpenApiInfo()
                          {
                              Title = "BlogAppAuthAPI",
                              Version = description.ApiVersion.ToString(),
                              Description = $"Blog App Auth API version {description.ApiVersion}"
                          });

        config.AddSecurityDefinition($"AuthToken {description.ApiVersion}",
                                     new OpenApiSecurityScheme
                                     {
                                         In = ParameterLocation.Header,
                                         Type = SecuritySchemeType.Http,
                                         BearerFormat = "JWT",
                                         Scheme = "bearer",
                                         Name = "Authorization",
                                         Description = "Authorization token"
                                     });

        config.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = $"AuthToken {description.ApiVersion}"
                    }
                },
                new string[] {}
            }
        });

        config.CustomOperationIds(description =>
            description.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
    }
});

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = "https://localhost:7090";
                   options.Audience = "BlogAppAuthAPI";
                   options.RequireHttpsMetadata = false;
               });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger(config =>
{
    config.RouteTemplate = "/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(config =>
{
    var provider = builder.Services.BuildServiceProvider();
    var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (ApiVersionDescription description in service.ApiVersionDescriptions)
    {
        config.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
    config.RoutePrefix = string.Empty;
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCustomExceptionHandler();
app.UseRouting();
app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
