using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetCommentsFromPost;

public class GetCommentFromPostQuery : IRequest<List<GetCommentFromPostDto>>
{
    public Guid PostId { get; set; }
}
