using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<IdentityResult>
{
    public string Name { get; set; }
}
