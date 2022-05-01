using BlogApp.Auth.Application.Users.Commands.LogoutUser;
using FluentValidation;

namespace BlogApp.Auth.Application.Users.Commands.LoginUser;

public class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommand>
{
    public LogoutUserCommandValidator()
    {
        RuleFor(logoutUserCommand => logoutUserCommand.UserName).Length(6, 25);
        RuleFor(logoutUserCommand => logoutUserCommand.Password).MinimumLength(6);
    }
}
