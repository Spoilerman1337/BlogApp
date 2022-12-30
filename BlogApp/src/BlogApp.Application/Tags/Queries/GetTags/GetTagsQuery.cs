using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetTags.Models;
using MediatR;

namespace BlogApp.Application.Tags.Queries.GetTags;

public class GetTagsQuery : IRequest<List<GetTagsDto>>, ICacheableQuery, IPaginatedQuery
{
    public bool BypassCache { get; set; }
    public string CacheKey => $"GetTags";
    public TimeSpan? SlidingExpiration { get; set; }
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
