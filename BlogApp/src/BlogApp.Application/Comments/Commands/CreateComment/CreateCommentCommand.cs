using MediatR;

namespace BlogApp.Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Text { get; set; }
}
