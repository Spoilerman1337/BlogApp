using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Meter;
using App.Metrics.Timer;
using MediatR;

namespace BlogApp.Application.Common.Behaviors;

public class MetricsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IMetricsRoot _metrics;

    public readonly TimerOptions _requestsExecutionTimer;
    public readonly MeterOptions _requests;
    public readonly MeterOptions _requestsExceptions;
    public readonly CounterOptions _requestsCounter;
    public readonly CounterOptions _requestsExceptionsCounter;

    public MetricsBehavior(IMetricsRoot metrics)
    {
        _metrics = metrics;

        _requestsExecutionTimer = new TimerOptions
        {
            Name = "Requests Execution Time",
            MeasurementUnit = App.Metrics.Unit.Requests,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Seconds,
            Context = "Application"
        };

        _requests = new MeterOptions
        {
            Name = "Requests",
            MeasurementUnit = App.Metrics.Unit.Requests,
            RateUnit = TimeUnit.Seconds,
            Context = "Application"
        };

        _requestsExceptions = new MeterOptions
        {
            Name = "Requests Exceptions",
            MeasurementUnit = App.Metrics.Unit.Requests,
            RateUnit = TimeUnit.Seconds,
            Context = "Application"
        };

        _requestsCounter = new CounterOptions
        {
            Name = "Requests Counter",
            MeasurementUnit = App.Metrics.Unit.Calls,
            Context = "Application"
        };

        _requestsExceptionsCounter = new CounterOptions
        {
            Name = "Request Exception Counter",
            MeasurementUnit = App.Metrics.Unit.Calls,
            Context = "Application"
        };
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var tags = new MetricTags("request", request.GetType().Name);

        _metrics.Measure.Counter.Increment(_requestsCounter);
        _metrics.Measure.Meter.Mark(_requests, tags);
        using (_metrics.Measure.Timer.Time(_requestsExecutionTimer, tags))
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _metrics.Measure.Counter.Increment(_requestsExceptionsCounter);
                _metrics.Measure.Meter.Mark(_requestsExceptions, new MetricTags(new string[] { "request", "exception" }, new string[] { request.GetType().Name, ex.GetType().FullName }));
                throw;
            }
        }
    }
}
