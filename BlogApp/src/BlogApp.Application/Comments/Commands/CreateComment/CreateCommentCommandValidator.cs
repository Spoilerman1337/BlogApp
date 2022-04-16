using FluentValidation;

namespace BlogApp.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(createCommentCommand => createCommentCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(createCommentCommand => createCommentCommand.Text).NotEmpty().MaximumLength(500);
        RuleFor(createCommentCommand => createCommentCommand.PostId).NotEqual(Guid.Empty);
    }
}
