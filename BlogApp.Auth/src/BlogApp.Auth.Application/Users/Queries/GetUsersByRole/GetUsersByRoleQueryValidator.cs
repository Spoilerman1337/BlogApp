using FluentValidation;

namespace BlogApp.Auth.Application.Users.Queries.GetUsersByRole;

public class GetUsersByRoleQueryValidator : AbstractValidator<GetUsersByRoleQuery>
{
    public GetUsersByRoleQueryValidator()
    {
        RuleFor(getUsersByRoleQuery => getUsersByRoleQuery.RoleId).NotEqual(Guid.Empty);
    }
}
