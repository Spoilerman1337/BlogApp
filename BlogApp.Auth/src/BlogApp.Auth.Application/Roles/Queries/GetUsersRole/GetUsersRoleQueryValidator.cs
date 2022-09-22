using FluentValidation;

namespace BlogApp.Auth.Application.Roles.Queries.GetUsersRole;

public class GetUsersRoleQueryValidator : AbstractValidator<GetUsersRoleQuery>
{
    public GetUsersRoleQueryValidator()
    {
        RuleFor(getUsersRoleQuery => getUsersRoleQuery.UserId).NotEqual(Guid.Empty);
    }
}
