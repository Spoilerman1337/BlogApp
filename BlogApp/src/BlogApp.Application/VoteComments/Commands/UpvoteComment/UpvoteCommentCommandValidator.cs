using FluentValidation;

namespace BlogApp.Application.VoteComments.Commands.UpvoteComment;

public class UpvoteCommentCommandValidator : AbstractValidator<UpvoteCommentCommand>
{
    public UpvoteCommentCommandValidator()
    {
        RuleFor(upvoteCommentCommand => upvoteCommentCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(upvoteCommentCommand => upvoteCommentCommand.CommentId).NotEqual(Guid.Empty);
    }
}
