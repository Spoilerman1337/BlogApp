using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostByComment.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPostByComment;

public class GetPostByCommentQuery : IRequest<GetPostByCommentDto>, ICacheableQuery
{
    public Guid CommentId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetPostByComment-{CommentId}";
    public TimeSpan? SlidingExpiration { get; set; }
}
