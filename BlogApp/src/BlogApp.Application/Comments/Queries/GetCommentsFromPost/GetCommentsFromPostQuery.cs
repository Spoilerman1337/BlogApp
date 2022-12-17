using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetCommentsFromPost;

public class GetCommentsFromPostQuery : IRequest<List<GetCommentsFromPostDto>>, ICacheableQuery, ISortableQuery
{
    public Guid PostId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetCommentsFromPost-{PostId}";
    public TimeSpan? SlidingExpiration { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
