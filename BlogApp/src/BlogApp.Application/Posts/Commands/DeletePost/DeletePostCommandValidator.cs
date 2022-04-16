using FluentValidation;

namespace BlogApp.Application.Posts.Commands.DeletePost;

public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(deletePostCommand => deletePostCommand.Id).NotEqual(Guid.Empty);
    }
}
