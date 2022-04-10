using FluentValidation;

namespace BlogApp.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(updatePostCommand => updatePostCommand.Id).NotEqual(Guid.Empty);
        RuleFor(updatePostCommand => updatePostCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(updatePostCommand => updatePostCommand.Header).NotEmpty().MaximumLength(50);
        RuleFor(updatePostCommand => updatePostCommand.Text).NotNull().MaximumLength(10000);
    }
}
