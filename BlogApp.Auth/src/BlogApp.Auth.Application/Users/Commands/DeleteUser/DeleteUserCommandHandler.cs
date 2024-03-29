﻿using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly UserManager<UserEntity> _userManager;

    public DeleteUserCommandHandler(UserManager<UserEntity> userManager) => _userManager = userManager;

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userManager.Users.Where(x => x.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(UserEntity), request.Id);
        }

        await _userManager.DeleteAsync(entity);
    }
}
