using FluentValidation;

namespace BlogApp.Auth.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(updateUserCommand => updateUserCommand.Id).NotEqual(Guid.Empty);
        RuleFor(updateUserCommand => updateUserCommand.UserName).Length(6, 25);
        RuleFor(updateUserCommand => updateUserCommand.Password).MinimumLength(6);
        RuleFor(updateUserCommand => updateUserCommand.Email).NotEmpty().EmailAddress();

        RuleFor(updateUserCommand => updateUserCommand.FirstName).MaximumLength(25);
        RuleFor(updateUserCommand => updateUserCommand.LastName).MaximumLength(25);
        RuleFor(updateUserCommand => updateUserCommand.Patronymic).MaximumLength(25);
    }
}
