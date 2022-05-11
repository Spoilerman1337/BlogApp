using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<IdentityResult>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
