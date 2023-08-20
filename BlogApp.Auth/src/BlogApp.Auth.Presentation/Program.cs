using BlogApp.Auth.Application;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure;
using BlogApp.Auth.Infrastructure.Persistance;
using BlogApp.Auth.Presentation.Middleware;
using BlogApp.Auth.Presentation.Options;
using BlogApp.Auth.Presentation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "BlogApp.Identity.Cookie";
    config.LoginPath = "/Authentication/Login";
    config.LogoutPath = "/Authentication/Logout";
});

builder.Services.AddIdentity<UserEntity, RoleEntity>(config =>
{
    config.Password.RequiredLength = 6;
    config.Password.RequireDigit = true;
    config.Password.RequireLowercase = true;
    config.Password.RequireUppercase = true;
    config.Password.RequireNonAlphanumeric = false;
})
                .AddEntityFrameworkStores<BlogAuthDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<UserEntity, RoleEntity, BlogAuthDbContext, Guid>>()
                .AddRoleStore<RoleStore<RoleEntity, BlogAuthDbContext, Guid>>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
    options.AddPolicy("ReleasePolicy", policy =>
    {
        policy.WithHeaders(builder.Configuration.GetSection("CORSPolicy")["AllowHeaders"]!.ToString());
        policy.WithMethods(builder.Configuration.GetSection("CORSPolicy")["AllowMethods"]!.ToString());
        policy.WithOrigins(builder.Configuration.GetSection("CORSPolicy")["AllowOrigins"]!.ToString());
        policy.AllowCredentials();
    });
});

builder.Services.AddControllersWithViews();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
   {
       options.Authority = "https://localhost:7090";
       options.Audience = "BlogAppAuthWebAPI";
       options.RequireHttpsMetadata = false;
       options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
   });
builder.Services.AddAuthorization();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseSwagger(config =>
{
    config.RouteTemplate = "/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(config =>
{
    var service = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
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
app.UseCors(environment == Environments.Development ? "DevelopmentPolicy" : "ReleasePolicy");
app.UseMetricsAllEndpoints();
app.UseStaticFiles();

app.UseCustomExceptionHandler();
app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
