﻿using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Roles.Commands.UpdateRole;
using BlogApp.Auth.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.Auth.Application.UnitTests.Roles.Commands;

public class UpdateRoleCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateRoleCommandHandler_Success()
    {
        //Arrange
        var handler = new UpdateRoleCommandHandler(RoleManager);
        string newRoleName = "Egestas";

        //Act
        await handler.Handle(
            new UpdateRoleCommand
            {
                Id = BlogAppContextFactory.ToBeUpdatedRoleId,
                Name = newRoleName
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Roles.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeUpdatedRoleId && c.Name == newRoleName)).Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateRoleCommandHandler_NoSuchRoleThrow()
    {
        //Arrange
        var handler = new UpdateRoleCommandHandler(RoleManager);
        string newRoleName = "Egestas";

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpdateRoleCommand
            {
                Id = Guid.NewGuid(),
                Name = newRoleName,
            },
            CancellationToken.None
        ));
    }
}
