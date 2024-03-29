﻿using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using BlogApp.Auth.Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<GetUsersDto>>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(UserManager<UserEntity> userManager, IMapper mapper) => (_userManager, _mapper) = (userManager, mapper);

    public async Task<List<GetUsersDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userManager.Users.Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                       .Take(request.PageAmount ?? int.MaxValue)
                                       .ProjectToType<GetUsersDto>()
                                       .ToListAsync(cancellationToken);
    }
}
