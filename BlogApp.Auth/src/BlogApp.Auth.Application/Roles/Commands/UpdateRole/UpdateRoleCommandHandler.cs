using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler
{
    private readonly RoleManager<AppRole> _roleManager;

    public UpdateRoleCommandHandler(RoleManager<AppRole> roleManager) => _roleManager = roleManager;

    public async Task<IdentityResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(AppRole), request.Id);
        }

        entity.Name = request.Name;

        return await _roleManager.UpdateAsync(entity);
    }
}
