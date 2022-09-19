using MediatR;

namespace BlogApp.Application.VoteComments.Commands.ChangeVoteComment;

public class ChangeVoteCommentCommand : IRequest
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
}
