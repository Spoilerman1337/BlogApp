using FluentValidation;

namespace BlogApp.Application.VotePosts.Commands.UnvotePost;

public class UnvotePostCommandValidator : AbstractValidator<UnvotePostCommand>
{
    public UnvotePostCommandValidator()
    {
        RuleFor(unvotePostCommand => unvotePostCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(unvotePostCommand => unvotePostCommand.PostId).NotEqual(Guid.Empty);
    }
}
