using BlogApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

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

        _logger.LogInformation("Handling {requestName} from userId {userId}", requestName, userId);

        var response = await next();

        return response;
    }
}
