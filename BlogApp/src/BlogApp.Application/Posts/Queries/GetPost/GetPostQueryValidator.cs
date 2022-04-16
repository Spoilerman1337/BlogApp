using FluentValidation;

namespace BlogApp.Application.Posts.Queries.GetPost;

public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
{
    public GetPostQueryValidator()
    {
        RuleFor(getPostQuery => getPostQuery.Id).NotEqual(Guid.Empty);
    }
}
