using MediatR;

namespace BlogApp.Application.VotePosts.Commands.UpvotePost;

public class UpvotePostCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public bool IsUpvoted { get; set; }
}
