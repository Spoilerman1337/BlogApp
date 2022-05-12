using MediatR;

namespace BlogApp.Auth.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
