using MediatR;

namespace BlogApp.Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Text { get; set; } = null!;
}
