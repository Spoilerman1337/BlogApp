using FluentValidation;

namespace BlogApp.Application.VotePosts.Commands.UpvotePost;

public class UpvotePostCommandValidator : AbstractValidator<UpvotePostCommand>
{
    public UpvotePostCommandValidator()
    {
        RuleFor(upvotePostCommand => upvotePostCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(upvotePostCommand => upvotePostCommand.PostId).NotEqual(Guid.Empty);
    }
}
