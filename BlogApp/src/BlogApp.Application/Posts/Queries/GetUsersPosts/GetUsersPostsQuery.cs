using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetUsersPosts.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts;

public class GetUsersPostsQuery : IRequest<List<GetUsersPostsDto>>, ICacheableQuery, IDateTimeFilterableQuery, IPaginatedQuery
{
    public Guid UserId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetPostsFromUser-{UserId}";
    public TimeSpan? SlidingExpiration { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
