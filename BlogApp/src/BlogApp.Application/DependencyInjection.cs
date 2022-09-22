using App.Metrics;
using App.Metrics.Filtering;
using App.Metrics.Formatters.Json;
using BlogApp.Application.Common.Behaviors;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Common.Mappings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new MappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new MappingProfile(typeof(IBlogDbContext).Assembly));
        });
        services.AddMetrics(AppMetrics.CreateDefaultBuilder().Report.ToConsole(options => {
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
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        return services;
    }
}
