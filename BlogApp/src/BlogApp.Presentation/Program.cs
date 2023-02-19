using BlogApp.Application;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Settings;
using BlogApp.Infrastructure;
using BlogApp.Presentation.Middleware.CustomExceptionHandler;
using BlogApp.Presentation.Options;
using BlogApp.Presentation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
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

builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.WithHeaders(builder.Configuration.GetSection("CORSPolicy")["AllowHeaders"]!.ToString());
        policy.WithMethods(builder.Configuration.GetSection("CORSPolicy")["AllowMethods"]!.ToString());
        policy.WithOrigins(builder.Configuration.GetSection("CORSPolicy")["AllowOrigins"]!.ToString());
    });
});

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
    config.DisplayRequestDuration();
});
app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("DevelopmentPolicy");
app.UseMetricsAllEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.UseApiVersioning();

app.MapControllers();

app.Run();
