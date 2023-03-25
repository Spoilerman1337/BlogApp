using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Authentication.Commands.LoginUser;

public class LoginUserCommand : IRequest<SignInResult>
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
