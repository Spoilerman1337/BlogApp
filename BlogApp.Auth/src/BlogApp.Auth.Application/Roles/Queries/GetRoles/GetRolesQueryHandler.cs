using BlogApp.Auth.Application.Roles.Queries.GetRoles.Models;
using BlogApp.Auth.Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Roles.Queries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<GetRolesDto>>
{
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IMapper _mapper;

    public GetRolesQueryHandler(RoleManager<RoleEntity> roleManager, IMapper mapper) => (_roleManager, _mapper) = (roleManager, mapper);

    public async Task<List<GetRolesDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return await _roleManager.Roles.Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                       .Take(request.PageAmount ?? int.MaxValue)
                                       .ProjectToType<GetRolesDto>()
                                       .ToListAsync(cancellationToken);
    }
}
