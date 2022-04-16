using BlogApp.Application.Posts.Queries.GetUsersPosts;
using FluentValidation;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts;

public class GetUsersPostsQueryValidator : AbstractValidator<GetUsersPostsQuery>
{
    public GetUsersPostsQueryValidator()
    {
        RuleFor(getPostQuery => getPostQuery.Id).NotEqual(Guid.Empty);
        RuleFor(getPostQuery => getPostQuery.UserId).NotEqual(Guid.Empty);
    }
}
