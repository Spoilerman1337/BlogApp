using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetCommentsFromPost;

public class GetCommentsFromPostQuery : IRequest<List<GetCommentsFromPostDto>>, ICacheableQuery
{
    public Guid PostId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetAllCommentsFromPost-{PostId}";
    public TimeSpan? SlidingExpiration { get; set; }
}
