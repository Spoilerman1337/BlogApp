using FluentValidation;

namespace BlogApp.Auth.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(createUserCommand => createUserCommand.Id).NotEqual(Guid.Empty);
        RuleFor(createUserCommand => createUserCommand.UserName).Length(6, 25);
        RuleFor(createUserCommand => createUserCommand.Password).MinimumLength(6);
        RuleFor(createUserCommand => createUserCommand.Email).NotEmpty().EmailAddress();
    }
}
