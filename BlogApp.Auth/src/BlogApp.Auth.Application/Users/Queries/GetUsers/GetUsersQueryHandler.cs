using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<GetUsersDto>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(UserManager<AppUser> userManager, IMapper mapper) => (_userManager, _mapper) = (userManager, mapper);

    public async Task<List<GetUsersDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userManager.Users.ProjectTo<GetUsersDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }
}
