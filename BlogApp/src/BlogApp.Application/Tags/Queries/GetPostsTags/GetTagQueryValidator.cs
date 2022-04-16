using FluentValidation;

namespace BlogApp.Application.Tags.Queries.GetPostsTags;

public class GetPostsTagQueryValidator : AbstractValidator<GetPostsTagsQuery>
{
    public GetPostsTagQueryValidator()
    {
        RuleFor(getTagQuery => getTagQuery.PostId).NotEqual(Guid.Empty);
    }
}
