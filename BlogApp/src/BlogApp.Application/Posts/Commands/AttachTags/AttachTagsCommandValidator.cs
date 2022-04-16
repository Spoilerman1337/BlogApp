using FluentValidation;

namespace BlogApp.Application.Posts.Commands.AttachTags;

public class AttachTagsCommandValidator : AbstractValidator<AttachTagsCommand>
{
    public AttachTagsCommandValidator()
    {
        RuleFor(attachTagsCommand => attachTagsCommand.Id).NotEqual(Guid.Empty);
        RuleFor(AttachTagsCommand => AttachTagsCommand.TagId).ForEach(tag => tag.NotEqual(Guid.Empty));
    }
}
