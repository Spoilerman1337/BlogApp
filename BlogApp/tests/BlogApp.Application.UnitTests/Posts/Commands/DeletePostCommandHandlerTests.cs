using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Posts.Commands.DeletePost;
using BlogApp.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Posts.Commands;

public class DeletePostCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeletePostCommandHandler_Success()
    {
        //Arrange
        var handler = new DeletePostCommandHandler(Context);

        //Act
        await handler.Handle(
            new DeletePostCommand
            {
                Id = BlogAppContextFactory.ToBeDeletedPostId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Posts.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeDeletedPostId)).Should().BeNull();
    }

    [Fact]
    public async Task DeletePostCommandHandler_WrongIdShouldThrow()
    {
        //Arrange
        var handler = new DeletePostCommandHandler(Context);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new DeletePostCommand
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }
}
