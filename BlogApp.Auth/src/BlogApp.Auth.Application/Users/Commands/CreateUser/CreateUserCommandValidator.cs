using FluentValidation;

namespace BlogApp.Auth.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(createUserCommand => createUserCommand.UserName).Length(6, 25);
        RuleFor(createUserCommand => createUserCommand.Password).MinimumLength(6);
        RuleFor(createUserCommand => createUserCommand.Email).NotEmpty().EmailAddress();

        RuleFor(createUserCommand => createUserCommand.FirstName).MaximumLength(25);
        RuleFor(createUserCommand => createUserCommand.LastName).MaximumLength(25);
        RuleFor(createUserCommand => createUserCommand.Patronymic).MaximumLength(25);
    }
}
