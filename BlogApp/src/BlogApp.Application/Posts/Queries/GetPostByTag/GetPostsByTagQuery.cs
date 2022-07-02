using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostByTag.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPostByTag;

public class GetPostsByTagQuery : IRequest<List<GetPostsByTagDto>>, ICacheableQuery
{
    public Guid TagId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetAllPostsByTag-{TagId}";
    public TimeSpan? SlidingExpiration { get; set; }
}
