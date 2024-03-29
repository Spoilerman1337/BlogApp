﻿using BlogApp.Auth.Application.Roles.Queries.GetRole.Models;
using BlogApp.Auth.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Roles.Queries.GetRole;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, GetRoleDto>
{
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IMapper _mapper;

    public GetRoleQueryHandler(RoleManager<RoleEntity> roleManager, IMapper mapper) => (_roleManager, _mapper) = (roleManager, mapper);

    public async Task<GetRoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var user = await _roleManager.FindByIdAsync(request.Id.ToString());

        return _mapper.Map<GetRoleDto>(user);
    }
}
