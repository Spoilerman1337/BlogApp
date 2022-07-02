using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPost.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPost;

public class GetPostQuery : IRequest<GetPostDto>, ICacheableQuery
{
    public Guid Id { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetPost-{Id}";
    public TimeSpan? SlidingExpiration { get; set; }
}
