using FluentValidation;

namespace BlogApp.Application.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(createPostCommand => createPostCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(createPostCommand => createPostCommand.Header).NotEmpty().MaximumLength(50);
        RuleFor(createPostCommand => createPostCommand.Text).MaximumLength(10000);
    }
}
