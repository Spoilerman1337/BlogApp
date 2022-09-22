using App.Metrics;
using App.Metrics.Filtering;
using App.Metrics.Formatters.Json;
using BlogApp.Auth.Application.Common.Behaviors;
using BlogApp.Auth.Application.Common.Identity;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Application.Common.Mappings;
using BlogApp.Auth.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogApp.Auth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new MappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new MappingProfile(typeof(IBlogAuthDbContext).Assembly));
        });
        services.AddMetrics(AppMetrics.CreateDefaultBuilder().Report.ToConsole(options =>
        {
            options.FlushInterval = TimeSpan.FromSeconds(5);
            options.Filter = new MetricsFilter().WhereType(MetricType.Timer);
            options.MetricsOutputFormatter = new MetricsJsonOutputFormatter();
        }).Build());
        services.AddMetricsEndpoints(c =>
        {
            c.MetricsEndpointOutputFormatter = new MetricsJsonOutputFormatter();
            c.MetricsTextEndpointOutputFormatter = new MetricsJsonOutputFormatter();
            c.EnvironmentInfoEndpointEnabled = false;
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MetricsBehavior<,>));
        services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
                .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                .AddInMemoryClients(IdentityConfiguration.Clients)
                .AddDeveloperSigningCredential();
        return services;
    }
}
