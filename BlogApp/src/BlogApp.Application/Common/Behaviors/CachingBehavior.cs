using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

namespace BlogApp.Application.Common.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ICacheableQuery
{
    private readonly IDistributedCache _cache;
    private readonly ILogger _logger;
    private readonly CacheSettings _settings;

    public CachingBehavior(IDistributedCache cache, ILogger<TResponse> logger, IOptions<CacheSettings> settings) => (_cache, _logger, _settings) = (cache, logger, settings.Value);

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response;
        byte[] cachedResponse;

        if (request.BypassCache)
            return await next();

        try
        {
            cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
        }
        catch (RedisException rex)
        {
            _logger.LogError("{message}", rex.Message);
            return await next();
        }

        if (cachedResponse != null)
        {
            response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cachedResponse));
            _logger.LogInformation("Fetched from Cache -> '{key}'.", request.CacheKey);
        }
        else
        {
            response = await GetResponseAndAddToCache(request, cancellationToken, next);
            _logger.LogInformation("Added to Cache -> '{key}'.", request.CacheKey);
        }
        return response;
    }

    private async Task<TResponse> GetResponseAndAddToCache(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        TResponse response = await next();

        var slidingExpiration = request.SlidingExpiration == null ? TimeSpan.FromHours(_settings.SlidingExpiration) : request.SlidingExpiration;
        var options = new DistributedCacheEntryOptions { SlidingExpiration = slidingExpiration };
        var serializedData = Encoding.Default.GetBytes(JsonConvert.SerializeObject(response));

        await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);

        return response;
    }
}