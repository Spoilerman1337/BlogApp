using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Users.Commands.SetUserRole;

public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;

    public SetUserRoleCommandHandler(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager) => (_userManager, _roleManager) = (userManager, roleManager);

    public async Task Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(x => x.Id == request.UserId).SingleOrDefaultAsync(cancellationToken);
        var role = await _roleManager.Roles.Where(x => x.Id == request.RoleId).SingleOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(UserEntity), request.UserId);
        }

        if (role == null)
        {
            throw new NotFoundException(nameof(RoleEntity), request.RoleId);
        }

        var oldRoles = await _userManager.GetRolesAsync(user);

        await _userManager.RemoveFromRolesAsync(user, oldRoles);
        await _userManager.AddToRoleAsync(user, role.Name!);
    }
}
