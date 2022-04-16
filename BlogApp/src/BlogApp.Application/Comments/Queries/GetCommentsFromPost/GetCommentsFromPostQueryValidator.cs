using FluentValidation;

namespace BlogApp.Application.Comments.Queries.GetCommentsFromPost;

public class GetCommentsFromPostQueryValidator : AbstractValidator<GetCommentsFromPostQuery>
{
    public GetCommentsFromPostQueryValidator()
    {
        RuleFor(getCommentFromPostQuery => getCommentFromPostQuery.PostId).NotEqual(Guid.Empty);
    }
}
