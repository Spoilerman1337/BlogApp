using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Users.Queries.GetUsersByRole.Models;
using BlogApp.Auth.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Users.Queries.GetUsersByRole;

public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQuery, List<GetUsersByRoleDto>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly IMapper _mapper;

    public GetUsersByRoleQueryHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
        => (_userManager, _roleManager, _mapper) = (userManager, roleManager, mapper);

    public async Task<List<GetUsersByRoleDto>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

        if (role == null)
            throw new NotFoundException(nameof(AppRole), request.RoleId);

        var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name!);

        if (usersInRole == null)
            return new();
        else
            return usersInRole.Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                              .Take(request.PageAmount ?? int.MaxValue)
                              .Select(c => _mapper.Map<GetUsersByRoleDto>(c))
                              .ToList();
    }
}
