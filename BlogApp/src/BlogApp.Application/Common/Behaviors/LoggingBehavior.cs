using BlogApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace BlogApp.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, ICurrentUserService currentUserService) => (_logger, _currentUserService) = (logger, currentUserService);

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = request.GetType().Name;
        var userId = _currentUserService.UserId;
        TResponse response;
        _logger.LogInformation("Handling {requestName} from userId {userId}", requestName, userId);

        //try
        //{
            response = await next();
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError("Exception {ex} occured during request handling", ex.Message);

        //    foreach (DictionaryEntry dataUnit in ex.Data)
        //    {
        //        _logger.LogDebug("Exception debug info (Data): {key} - {value}", dataUnit.Key, dataUnit.Value);
        //    }

        //    _logger.LogDebug("Exception debug info (Stack trace): {stacktrace}", ex.StackTrace);
        //    _logger.LogDebug("Exception debug info (Source): {source}", ex.Source);
        //}
        //finally
        //{
            return response;
        //}
    }
}
