using FluentValidation;

namespace BlogApp.Application.Posts.Queries.GetPosts;

public class GetPostsQueryValidator : AbstractValidator<GetPostsQuery>
{
    public GetPostsQueryValidator()
    {
        RuleFor(getPostsQuery => getPostsQuery.UserId).NotEqual(Guid.Empty);
    }
}
