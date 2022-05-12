using MediatR;

namespace BlogApp.Auth.Application.Roles.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest
{
    public Guid Id { get; set; }
}
