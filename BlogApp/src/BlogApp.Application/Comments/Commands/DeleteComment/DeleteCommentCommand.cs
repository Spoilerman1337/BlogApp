using MediatR;

namespace BlogApp.Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommand : IRequest
{
    public Guid Id { get; set; }
}
