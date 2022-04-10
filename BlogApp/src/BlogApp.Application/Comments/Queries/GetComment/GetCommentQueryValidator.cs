using FluentValidation;

namespace BlogApp.Application.Comments.Queries.GetComment;

public class GetCommentQueryValidator : AbstractValidator<GetCommentQuery>
{
    public GetCommentQueryValidator()
    {
        RuleFor(getCommentQuery => getCommentQuery.Id).NotEqual(Guid.Empty);
        RuleFor(getCommentQuery => getCommentQuery.UserId).NotEqual(Guid.Empty);
    }
}
