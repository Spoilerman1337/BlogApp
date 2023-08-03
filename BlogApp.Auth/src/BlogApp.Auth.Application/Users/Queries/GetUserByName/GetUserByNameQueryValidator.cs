using FluentValidation;

namespace BlogApp.Auth.Application.Users.Queries.GetUserByName;

public class GetUserByNameQueryValidator : AbstractValidator<GetUserByNameQuery>
{
    public GetUserByNameQueryValidator()
    {
        RuleFor(getUserByNameQuery => getUserByNameQuery.UserName).NotEmpty();
        RuleFor(getUserByNameQuery => getUserByNameQuery.UserName).NotNull();
        RuleFor(getUserByNameQuery => getUserByNameQuery.UserName).Length(6, 25);
    }
}
