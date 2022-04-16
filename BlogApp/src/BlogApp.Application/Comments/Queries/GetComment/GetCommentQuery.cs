using BlogApp.Application.Comments.Queries.GetComment.Models;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetComment;

public class GetCommentQuery : IRequest<GetCommentDto>
{
    public Guid Id { get; set; }
}
