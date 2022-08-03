using MediatR;

namespace BlogApp.Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public string Text { get; set; }
    public Guid? ParentCommentId { get; set; }
}
