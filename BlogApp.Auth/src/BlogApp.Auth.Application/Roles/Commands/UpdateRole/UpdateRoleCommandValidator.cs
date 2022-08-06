using FluentValidation;

namespace BlogApp.Auth.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(updateRoleCommand => updateRoleCommand.Id).NotEqual(Guid.Empty);
        RuleFor(updateRoleCommand => updateRoleCommand.Name).Length(6, 25);
    }
}
