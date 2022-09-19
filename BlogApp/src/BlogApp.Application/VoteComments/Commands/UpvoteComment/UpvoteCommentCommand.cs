using MediatR;

namespace BlogApp.Application.VoteComments.Commands.UpvoteComment;

public class UpvoteCommentCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid CommentId { get; set; }
    public bool IsUpvoted { get; set; }
}
