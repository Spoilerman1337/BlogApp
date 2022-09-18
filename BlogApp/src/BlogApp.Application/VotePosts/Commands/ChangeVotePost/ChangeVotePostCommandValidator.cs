using BlogApp.Application.VotePosts.Commands.UnvotePost;
using FluentValidation;

namespace BlogApp.Application.VotePosts.Commands.ChangeVotePost;

public class ChangeVotePostCommandValidator : AbstractValidator<UnvotePostCommand>
{
    public ChangeVotePostCommandValidator()
    {
        RuleFor(changeVotePostCommand => changeVotePostCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(changeVotePostCommand => changeVotePostCommand.PostId).NotEqual(Guid.Empty);
    }
}
