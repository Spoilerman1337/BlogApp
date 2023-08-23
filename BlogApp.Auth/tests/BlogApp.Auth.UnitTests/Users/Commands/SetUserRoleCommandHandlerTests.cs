using System;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Users.Commands.SetUserRole;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Auth.UnitTests.Users.Commands;

public class SetUserRoleCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task SetUserRoleCommandHandler_Success()
    {
        //Arrange
        var handler = new SetUserRoleCommandHandler(UserManager, RoleManager);
        var newRoleId = Guid.Parse("5C29FCAB-1AAD-4EDD-8D75-3770D98D651D");
        var userId = Guid.Parse("FDAE39CF-B55E-47BC-9904-00DF3CFD6FAD");

        //Act
        await handler.Handle(
            new SetUserRoleCommand
            {
                RoleId = newRoleId,
                UserId = userId
            },
            CancellationToken.None);

        //Assert
        (await UserManager.GetRolesAsync((await Context.Users.SingleOrDefaultAsync(c => c.Id == userId))!)).Should()
            .Contain("Aliquet").And.HaveCount(1);
    }

    [Fact]
    public async Task SetUserRoleCommandHandler_NoSuchUserThrow()
    {
        //Arrange
        var handler = new SetUserRoleCommandHandler(UserManager, RoleManager);
        var newRoleId = Guid.Parse("5C29FCAB-1AAD-4EDD-8D75-3770D98D651D");
        var userId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new SetUserRoleCommand
            {
                RoleId = newRoleId,
                UserId = userId
            },
            CancellationToken.None)
        );
    }

    [Fact]
    public async Task SetUserRoleCommandHandler_NoSuchRoleThrow()
    {
        //Arrange
        var handler = new SetUserRoleCommandHandler(UserManager, RoleManager);
        var newRoleId = Guid.NewGuid();
        var userId = Guid.Parse("FDAE39CF-B55E-47BC-9904-00DF3CFD6FAD");

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new SetUserRoleCommand
            {
                RoleId = newRoleId,
                UserId = userId
            },
            CancellationToken.None)
        );
    }
}