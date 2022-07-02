using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPosts.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPosts;

public class GetPostsQuery : IRequest<List<GetPostsDto>>, ICacheableQuery
{
    public bool BypassCache { get; set; }
    public string CacheKey => $"GetAllPosts";
    public TimeSpan? SlidingExpiration { get; set; }
}
