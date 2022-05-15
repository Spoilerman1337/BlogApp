using BlogApp.Application;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Infrastructure;
using BlogApp.Presentation.Middleware;
using BlogApp.Presentation.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

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

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(config =>
{
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
