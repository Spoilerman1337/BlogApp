using FluentValidation;

namespace BlogApp.Application.Tags.Commands.CreateTag;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(createTagCommand => createTagCommand.TagName).NotEmpty().Length(1, 25);
    }
}
