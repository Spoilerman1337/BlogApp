using FluentValidation;

namespace BlogApp.Application.Tags.Commands.DeleteTag;

public class DeleteTagCommandValidator : AbstractValidator<DeleteTagCommand>
{
    public DeleteTagCommandValidator()
    {
        RuleFor(deleteTagCommand => deleteTagCommand.Id).NotEqual(Guid.Empty);
    }
}
