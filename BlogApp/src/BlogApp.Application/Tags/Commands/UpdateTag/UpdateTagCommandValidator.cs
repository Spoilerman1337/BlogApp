using FluentValidation;

namespace BlogApp.Application.Tags.Commands.UpdateTag;

public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagCommandValidator()
    {
        RuleFor(updateTagCommand => updateTagCommand.Id).NotEqual(Guid.Empty);
        RuleFor(updateTagCommand => updateTagCommand.TagName).NotEmpty().Length(1, 20);
    }
}
