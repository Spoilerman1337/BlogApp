using FluentValidation;

namespace BlogApp.Application.Posts.Queries.GetPostByComment;

public class GetPostByCommentQueryValidator : AbstractValidator<GetPostByCommentQuery>
{
    public GetPostByCommentQueryValidator()
    {
        RuleFor(getPostQuery => getPostQuery.CommentId).NotEqual(Guid.Empty);
    }
}
