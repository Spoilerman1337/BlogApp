﻿using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Authentication.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;

    public RegisterUserCommandHandler(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager) => (_userManager, _roleManager) = (userManager, roleManager);

    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic
        };

        var appRole = (await _roleManager.FindByIdAsync(request.RoleId.ToString()));
        string roleName;

        if (appRole != null)
            roleName = appRole.Name!;
        else
            throw new NotFoundException(nameof(RoleEntity), request.RoleId);

        var result = await _userManager.CreateAsync(user, request.Password);

        await _userManager.AddToRoleAsync(user, roleName);

        return result;
    }
}
