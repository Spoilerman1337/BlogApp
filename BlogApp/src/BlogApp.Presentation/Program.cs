using BlogApp.Application;
using BlogApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
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
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseApiVersioning();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
