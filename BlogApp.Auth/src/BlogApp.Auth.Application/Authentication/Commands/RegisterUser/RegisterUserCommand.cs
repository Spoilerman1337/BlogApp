using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Authentication.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<IdentityResult>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public Guid RoleId { get; set; }
}
