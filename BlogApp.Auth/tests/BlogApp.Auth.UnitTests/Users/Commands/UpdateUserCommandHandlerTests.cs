using System;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Users.Commands.UpdateUser;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Auth.UnitTests.Users.Commands;

public class UpdateUserCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateUserCommandHandler_Success()
    {
        //Arrange
        var handler = new UpdateUserCommandHandler(UserManager);
        var newUserName = "Turpis";
        var newPassword = "Consectetur123";
        var newEmail = "id@lorem.com";
        var newFirstName = "Et";
        var newLastName = "Sit";
        var newPatronymic = "Aenean";

        //Act
        await handler.Handle(
            new UpdateUserCommand
            {
                Id = BlogAppContextFactory.ToBeUpdatedUserId,
                UserName = newUserName,
                Password = newPassword,
                Email = newEmail,
                FirstName = newFirstName,
                LastName = newLastName,
                Patronymic = newPatronymic
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Users.SingleOrDefaultAsync(c =>
            c.Id == BlogAppContextFactory.ToBeUpdatedUserId && c.UserName == newUserName)).Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateUserCommandHandler_NoSuchUserThrow()
    {
        //Arrange
        var handler = new UpdateUserCommandHandler(UserManager);
        var newUserName = "Turpis";
        var newPassword = "Consectetur123";
        var newEmail = "id@lorem.com";
        var newFirstName = "Et";
        var newLastName = "Sit";
        var newPatronymic = "Aenean";

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpdateUserCommand
            {
                Id = Guid.NewGuid(),
                UserName = newUserName,
                Password = newPassword,
                Email = newEmail,
                FirstName = newFirstName,
                LastName = newLastName,
                Patronymic = newPatronymic
            },
            CancellationToken.None
        ));
    }
}