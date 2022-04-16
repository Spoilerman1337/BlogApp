using FluentValidation;

namespace BlogApp.Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(deleteCommentCommand => deleteCommentCommand.Id).NotEqual(Guid.Empty);
    }
}
