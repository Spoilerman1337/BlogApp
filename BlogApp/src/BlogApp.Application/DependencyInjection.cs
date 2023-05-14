using App.Metrics;
using App.Metrics.Filtering;
using App.Metrics.Formatters.Json;
using BlogApp.Application.Common.Behaviors;
using FluentValidation;
using Mapster;
using Mapster.Utils;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

        services.AddMapster();
        TypeAdapterConfig.GlobalSettings.ScanInheritedTypes(Assembly.GetExecutingAssembly());

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
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        return services;
    }
}
