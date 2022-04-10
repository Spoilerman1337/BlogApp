using FluentValidation;

namespace BlogApp.Application.Tags.Queries.GetTag;

public class GetTagQueryValidator : AbstractValidator<GetTagQuery>
{
    public GetTagQueryValidator()
    {
        RuleFor(getTagQuery => getTagQuery.Id).NotEqual(Guid.Empty);
    }
}
