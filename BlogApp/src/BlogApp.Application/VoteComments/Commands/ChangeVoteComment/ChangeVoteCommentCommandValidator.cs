using FluentValidation;

namespace BlogApp.Application.VoteComments.Commands.ChangeVoteComment;

public class ChangeVoteCommentCommandValidator : AbstractValidator<ChangeVoteCommentCommand>
{
    public ChangeVoteCommentCommandValidator()
    {
        RuleFor(changeVoteCommentCommand => changeVoteCommentCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(changeVoteCommentCommand => changeVoteCommentCommand.CommentId).NotEqual(Guid.Empty);
    }
}
