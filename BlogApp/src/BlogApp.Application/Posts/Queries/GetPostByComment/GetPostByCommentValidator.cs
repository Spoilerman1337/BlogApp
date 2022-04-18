using FluentValidation;

namespace BlogApp.Application.Posts.Queries.GetPostByComment;

public class GetPostByCommentValidator : AbstractValidator<GetPostByCommentQuery>
{
    public GetPostByCommentValidator()
    {
        RuleFor(getPostQuery => getPostQuery.CommentId).NotEqual(Guid.Empty);
    }
}
