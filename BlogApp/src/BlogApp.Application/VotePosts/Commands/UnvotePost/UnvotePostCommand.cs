using MediatR;

namespace BlogApp.Application.VotePosts.Commands.UnvotePost;

public class UnvotePostCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}
