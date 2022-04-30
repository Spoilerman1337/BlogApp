using AutoMapper;
using BlogApp.Auth.Application.Users.Queries.GetUser.Models;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(UserManager<AppUser> userManager, IMapper mapper) => (_userManager, _mapper) = (userManager, mapper);

    public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());

        return _mapper.Map<GetUserDto>(user);
    }
}
