using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Roles.Commands.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly RoleManager<AppRole> _roleManager;

    public UpdateRoleCommandHandler(RoleManager<AppRole> roleManager) => _roleManager = roleManager;

    public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _roleManager.Roles.Where(x => x.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(AppRole), request.Id);
        }

        entity.Name = request.Name;

        await _roleManager.UpdateAsync(entity);

        return Unit.Value;
    }
}
