using BlogApp.Auth.Application.Roles.Commands.CreateRole;
using BlogApp.Auth.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.Auth.Application.UnitTests.Roles.Commands;

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
