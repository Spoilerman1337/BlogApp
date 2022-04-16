using FluentValidation;

namespace BlogApp.Application.Posts.Commands.DetachTags;

public class DetachTagsCommandValidator : AbstractValidator<DetachTagsCommand>
{
    public DetachTagsCommandValidator()
    {
        RuleFor(attachTagsCommand => attachTagsCommand.Id).NotEqual(Guid.Empty);
        RuleFor(AttachTagsCommand => AttachTagsCommand.TagId).ForEach(tag => tag.NotEqual(Guid.Empty));
    }
}
