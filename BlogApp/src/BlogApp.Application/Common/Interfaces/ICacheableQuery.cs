namespace BlogApp.Application.Common.Interfaces;

public interface ICacheableQuery
{
    bool BypassCache { get; }
    string CacheKey { get; }
    TimeSpan? SlidingExpiration { get; }
}
