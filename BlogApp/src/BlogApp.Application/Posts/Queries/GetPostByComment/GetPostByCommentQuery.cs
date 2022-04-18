using BlogApp.Application.Posts.Queries.GetPostByComment.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPostByComment;

public class GetPostByCommentQuery : IRequest<GetPostByCommentDto>
{
    public Guid CommentId { get; set; }
}
