using FluentValidation;

namespace BlogApp.Application.VotePosts.Commands.ChangeVotePost;

public class ChangeVotePostCommandValidator : AbstractValidator<ChangeVotePostCommand>
{
    public ChangeVotePostCommandValidator()
    {
        RuleFor(changeVotePostCommand => changeVotePostCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(changeVotePostCommand => changeVotePostCommand.PostId).NotEqual(Guid.Empty);
    }
}
