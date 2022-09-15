using FluentValidation;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts;

public class GetUsersPostsQueryValidator : AbstractValidator<GetUsersPostsQuery>
{
    public GetUsersPostsQueryValidator()
    {
        RuleFor(getPostQuery => getPostQuery.UserId).NotEqual(Guid.Empty);
    }
}
