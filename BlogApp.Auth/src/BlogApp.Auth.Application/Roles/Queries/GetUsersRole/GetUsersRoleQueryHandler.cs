using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Roles.Queries.GetUsersRole.Models;
using BlogApp.Auth.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Roles.Queries.GetUsersRole;

public class GetUsersRoleQueryHandler : IRequestHandler<GetUsersRoleQuery, GetUsersRoleDto>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IMapper _mapper;

    public GetUsersRoleQueryHandler(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IMapper mapper)
        => (_userManager, _roleManager, _mapper) = (userManager, roleManager, mapper);

    public async Task<GetUsersRoleDto> Handle(GetUsersRoleQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user == null)
            throw new NotFoundException(nameof(UserEntity), request.UserId);

        var roleName = await _userManager.GetRolesAsync(user);
        var role = await _roleManager.FindByNameAsync(roleName.First());

        return _mapper.Map<GetUsersRoleDto>(role);
    }
}
