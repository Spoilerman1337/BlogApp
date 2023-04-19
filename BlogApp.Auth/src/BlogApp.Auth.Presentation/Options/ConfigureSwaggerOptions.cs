using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace BlogApp.Auth.Presentation.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

        foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName,
                              new OpenApiInfo()
                              {
                                  Title = "BlogAppAuthAPI",
                                  Version = description.ApiVersion.ToString(),
                                  Description = $"Blog App Auth API version {description.ApiVersion}"
                              });

            options.AddSecurityDefinition($"AuthToken {description.ApiVersion}",
                                         new OpenApiSecurityScheme
                                         {
                                             In = ParameterLocation.Header,
                                             Type = SecuritySchemeType.Http,
                                             BearerFormat = "JWT",
                                             Scheme = "bearer",
                                             Name = "Authorization",
                                             Description = "Authorization token"
                                         });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                    Array.Empty<string>()
                }
            });

            options.CustomOperationIds(description =>
                description.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
        }
    }
}
