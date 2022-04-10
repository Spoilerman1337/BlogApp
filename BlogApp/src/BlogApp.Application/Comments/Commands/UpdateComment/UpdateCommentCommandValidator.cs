using FluentValidation;

namespace BlogApp.Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(updateCommentCommand => updateCommentCommand.Id).NotEqual(Guid.Empty);
        RuleFor(updateCommentCommand => updateCommentCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(updateCommentCommand => updateCommentCommand.Text).NotEmpty().MaximumLength(500);
    }
}
