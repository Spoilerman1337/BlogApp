using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetTag.Models;
using MediatR;

namespace BlogApp.Application.Tags.Queries.GetTag;

public class GetTagQuery : IRequest<GetTagDto>, ICacheableQuery
{
    public Guid Id { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetTag-{Id}";
    public TimeSpan? SlidingExpiration { get; set; }
}
