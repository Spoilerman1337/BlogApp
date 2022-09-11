using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.UnitTests.Common;
using BlogApp.Auth.Application.Users.Commands.DeleteUser;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.Auth.Application.UnitTests.Users.Commands;

public class DeleteUserCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteUserCommandHandler_Success()
    {
        //Arrange
        var handler = new DeleteUserCommandHandler(UserManager);

        //Act
        await handler.Handle(
            new DeleteUserCommand
            {
                Id = BlogAppContextFactory.ToBeDeletedUserId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Users.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeDeletedUserId)).Should().BeNull();
    }

    [Fact]
    public async Task DeleteUserCommandHandler_NoSuchUserThrow()
    {
        //Arrange
        var handler = new DeleteUserCommandHandler(UserManager);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new DeleteUserCommand
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }
}
