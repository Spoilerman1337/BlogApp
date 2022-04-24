using MediatR;

namespace BlogApp.Auth.Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }
}
