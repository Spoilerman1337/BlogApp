using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, IdentityResult>
{
    private readonly RoleManager<AppRole> _roleManager;

    public CreateRoleCommandHandler(RoleManager<AppRole> roleManager) => _roleManager = roleManager;

    public async Task<IdentityResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new AppRole
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        var result = await _roleManager.CreateAsync(role);

        return result;
    }
}
