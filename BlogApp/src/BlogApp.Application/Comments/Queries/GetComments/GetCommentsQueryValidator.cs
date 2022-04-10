using FluentValidation;

namespace BlogApp.Application.Comments.Queries.GetComments;

public class GetCommentsQueryValidator : AbstractValidator<GetCommentsQuery>
{
    public GetCommentsQueryValidator()
    {
        RuleFor(getCommentsQuery => getCommentsQuery.UserId).NotEqual(Guid.Empty);
    }
}
