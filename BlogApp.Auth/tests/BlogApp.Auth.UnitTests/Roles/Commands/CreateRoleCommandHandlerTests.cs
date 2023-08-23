using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Roles.Commands.CreateRole;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Auth.UnitTests.Roles.Commands;

public class CreateRoleCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateRoleCommandHandler_Success()
    {
        //Arrange
        var handler = new CreateRoleCommandHandler(RoleManager);
        var roleName = "Turpis";

        //Act
        var identityResult = await handler.Handle(
            new CreateRoleCommand
            {
                Name = roleName
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Roles.SingleOrDefaultAsync(c => c.Name == roleName)).Should().NotBeNull();
    }
}