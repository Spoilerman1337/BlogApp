using FluentValidation;

namespace BlogApp.Auth.Application.Roles.Queries.GetRole;

public class GetRoleQueryValidator : AbstractValidator<GetRoleQuery>
{
    public GetRoleQueryValidator()
    {
        RuleFor(getRoleQuery => getRoleQuery.Id).NotEqual(Guid.Empty);
    }
}
