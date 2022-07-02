using BlogApp.Application.Comments.Queries.GetComment.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetComment;

public class GetCommentQuery : IRequest<GetCommentDto>, ICacheableQuery
{
    public Guid Id { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetComment-{Id}";
    public TimeSpan? SlidingExpiration { get; set; }
}
