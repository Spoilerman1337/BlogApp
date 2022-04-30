using FluentValidation;

namespace BlogApp.Auth.Application.Users.Queries.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(getUserQuery => getUserQuery.Id).NotEqual(Guid.Empty);
    }
}
