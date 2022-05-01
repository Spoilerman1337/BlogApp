using MediatR;

namespace BlogApp.Auth.Application.Users.Commands.LogoutUser;

public class LogoutUserCommand : IRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
