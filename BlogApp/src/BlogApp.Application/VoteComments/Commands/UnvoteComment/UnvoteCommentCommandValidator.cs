using FluentValidation;

namespace BlogApp.Application.VoteComments.Commands.UnvoteComment;

public class UnvoteCommentCommandValidator : AbstractValidator<UnvoteCommentCommand>
{
    public UnvoteCommentCommandValidator()
    {
        RuleFor(unvoteCommentCommand => unvoteCommentCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(unvoteCommentCommand => unvoteCommentCommand.CommentId).NotEqual(Guid.Empty);
    }
}
