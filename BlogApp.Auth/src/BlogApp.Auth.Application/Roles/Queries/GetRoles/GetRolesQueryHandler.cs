using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Auth.Application.Roles.Queries.GetRoles.Models;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<GetRolesDto>>
{
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(RoleManager<AppRole> roleManager, IMapper mapper) => (_roleManager, _mapper) = (roleManager, mapper);

    public async Task<List<GetRolesDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _roleManager.Roles.ProjectTo<GetRolesDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
