using FluentValidation;

namespace BlogApp.Auth.Application.Authentication.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(registerUserCommand => registerUserCommand.UserName).Length(6, 25);
        RuleFor(registerUserCommand => registerUserCommand.Password).MinimumLength(6);
    }
}
