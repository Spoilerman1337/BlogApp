using MediatR;

namespace BlogApp.Application.VotePosts.Commands.ChangeVotePost;

public class ChangeVotePostCommand : IRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
}
