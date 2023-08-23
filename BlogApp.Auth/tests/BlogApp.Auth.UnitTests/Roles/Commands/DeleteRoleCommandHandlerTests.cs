using System;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Roles.Commands.DeleteRole;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Auth.UnitTests.Roles.Commands;

public class DeleteRoleCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteRoleCommandHandler_Success()
    {
        //Arrange
        var handler = new DeleteRoleCommandHandler(RoleManager);

        //Act
        await handler.Handle(
            new DeleteRoleCommand
            {
                Id = BlogAppContextFactory.ToBeDeletedRoleId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Roles.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeDeletedRoleId)).Should()
            .BeNull();
    }

    [Fact]
    public async Task DeleteRoleCommandHandler_NoSuchRoleThrow()
    {
        //Arrange
        var handler = new DeleteRoleCommandHandler(RoleManager);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new DeleteRoleCommand
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }
}