using BlogApp.Auth.Application.Users.Queries.GetUserByName.Models;
using BlogApp.Auth.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Users.Queries.GetUserByName;

public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, GetUserByNameDto>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _mapper;

    public GetUserByNameQueryHandler(UserManager<UserEntity> userManager, IMapper mapper) => (_userManager, _mapper) = (userManager, mapper);

    public async Task<GetUserByNameDto> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        return _mapper.Map<GetUserByNameDto>(user);
    }
}
