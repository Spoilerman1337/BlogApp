using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetPostsTags.Models;
using MediatR;

namespace BlogApp.Application.Tags.Queries.GetPostsTags;

public class GetPostsTagsQuery : IRequest<List<GetPostsTagsDto>>, ICacheableQuery, IPaginatedQuery
{
    public Guid PostId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetPostsTags-{PostId}";
    public TimeSpan? SlidingExpiration { get; set; }
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
