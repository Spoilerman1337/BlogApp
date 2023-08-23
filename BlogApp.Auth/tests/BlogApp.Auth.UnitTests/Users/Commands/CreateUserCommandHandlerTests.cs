using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Users.Commands.CreateUser;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Auth.UnitTests.Users.Commands;

public class CreateUserCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateUserCommandHandler_Success()
    {
        //Arrange
        var handler = new CreateUserCommandHandler(UserManager);
        var userName = "Vel";
        var password = "Fermentum123";
        var email = "porttitor@lorem.com";
        var firstName = "Maecenas";
        var lastName = "Pretium";
        var patronymic = "Purus";

        //Act
        var identityResult = await handler.Handle(
            new CreateUserCommand
            {
                UserName = userName,
                Password = password,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Users.SingleOrDefaultAsync(c => c.UserName == userName)).Should().NotBeNull();
    }
}