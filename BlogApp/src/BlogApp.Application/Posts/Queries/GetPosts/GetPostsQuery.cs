using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPosts.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPosts;

public class GetPostsQuery : IRequest<List<GetPostsDto>>, ICacheableQuery, ISortableQuery
{
    public bool BypassCache { get; set; }
    public string CacheKey => $"GetPosts";
    public TimeSpan? SlidingExpiration { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
