using FluentValidation;

namespace BlogApp.Application.Comments.Queries.GetUsersComments;

public class GetUsersCommentsValidator : AbstractValidator<GetUsersCommentsQuery>
{
    public GetUsersCommentsValidator()
    {
        RuleFor(getCommentQuery => getCommentQuery.UserId).NotEqual(Guid.Empty);
    }
}
