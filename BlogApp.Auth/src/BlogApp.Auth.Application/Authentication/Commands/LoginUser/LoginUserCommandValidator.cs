using FluentValidation;

namespace BlogApp.Auth.Application.Authentication.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(loginUserCommand => loginUserCommand.UserName).Length(6, 25);
        RuleFor(loginUserCommand => loginUserCommand.Password).MinimumLength(6);
    }
}
