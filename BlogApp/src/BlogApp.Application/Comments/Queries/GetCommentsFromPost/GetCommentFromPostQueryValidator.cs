using FluentValidation;

namespace BlogApp.Application.Comments.Queries.GetCommentsFromPost;

public class GetCommentFromPostQueryValidator : AbstractValidator<GetCommentFromPostQuery>
{
    public GetCommentFromPostQueryValidator()
    {
        RuleFor(getCommentFromPostQuery => getCommentFromPostQuery.PostId).NotEqual(Guid.Empty);
    }
}
