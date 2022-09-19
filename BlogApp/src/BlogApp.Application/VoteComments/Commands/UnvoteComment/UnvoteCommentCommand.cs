using MediatR;

namespace BlogApp.Application.VoteComments.Commands.UnvoteComment;

public class UnvoteCommentCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid CommentId { get; set; }
}
