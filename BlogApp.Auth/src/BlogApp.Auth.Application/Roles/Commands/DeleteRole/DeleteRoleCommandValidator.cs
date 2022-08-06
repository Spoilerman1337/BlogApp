using FluentValidation;

namespace BlogApp.Auth.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(deleteRoleCommand => deleteRoleCommand.Id).NotEqual(Guid.Empty);
    }
}
