using FluentValidation;

namespace BlogApp.Auth.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(createRoleCommand => createRoleCommand.Name).Length(6, 25);
    }
}
