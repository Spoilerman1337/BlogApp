using MediatR;

namespace BlogApp.Auth.Application.Users.Commands.SetUserRole;

public class SetUserRoleCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
