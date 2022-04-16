using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetCommentsFromPost;

public class GetCommentsFromPostQuery : IRequest<List<GetCommentsFromPostDto>>
{
    public Guid PostId { get; set; }
}
