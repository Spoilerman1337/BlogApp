using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetUsersPosts.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts;

public class GetUsersPostsQuery : IRequest<List<GetUsersPostsDto>>, ICacheableQuery
{
    public Guid UserId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetAllPostsFromUser-{UserId}";
    public TimeSpan? SlidingExpiration { get; set; }
}
