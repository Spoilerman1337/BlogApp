using BlogApp.Application;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Settings;
using BlogApp.Infrastructure;
using BlogApp.Presentation.Middleware;
using BlogApp.Presentation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

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
                              Title = "BlogAppAPI",
                              Version = description.ApiVersion.ToString(),
                              Description = $"Blog App API version {description.ApiVersion}"
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
                   options.Authority = "https://localhost:7090/";
                   options.Audience = "BlogAppWebAPI";
                   options.RequireHttpsMetadata = false;
               });

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

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
    config.DisplayRequestDuration();
});
app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseApiVersioning();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
