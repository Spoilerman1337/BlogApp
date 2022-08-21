using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Posts.Commands.UpdatePost;
using BlogApp.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Posts.Commands;

public class UpdatePostCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdatePostCommandHandler_Success()
    {
        //Arrange
        var handler = new UpdatePostCommandHandler(Context);
        string newText = "Vestibulum ante ipsum primis in faucibus orci luctus.";
        string newHeader = "Sed vel molestie lectus. Suspendisse posuere sodales nibh.";

        //Act
        await handler.Handle(
            new UpdatePostCommand
            {
                Id = BlogAppContextFactory.ToBeUpdatedPostId,
                Text = newText,
                UserId = BlogAppContextFactory.UserAId,
                Header = newHeader,
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Posts.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeUpdatedPostId && c.Text == newText && c.Header == newHeader)).Should().NotBeNull();
    }

    [Fact]
    public async Task UpdatePostCommandHandler_WrongUserShouldThrow()
    {
        //Arrange
        var handler = new UpdatePostCommandHandler(Context);
        string newText = "Vestibulum ante ipsum primis in faucibus orci luctus.";
        string newHeader = "Sed vel molestie lectus. Suspendisse posuere sodales nibh.";

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpdatePostCommand
            {
                Id = BlogAppContextFactory.ToBeUpdatedPostId,
                Text = newText,
                UserId = BlogAppContextFactory.UserBId,
                Header = newHeader,
            },
            CancellationToken.None
        ));
    }

    [Fact]
    public async Task UpdatePostCommandHandler_WrongIdShouldThrow()
    {
        //Arrange
        var handler = new UpdatePostCommandHandler(Context);
        string newText = "Vestibulum ante ipsum primis in faucibus orci luctus.";
        string newHeader = "Sed vel molestie lectus. Suspendisse posuere sodales nibh.";

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpdatePostCommand
            {
                Id = Guid.NewGuid(),
                Text = newText,
                UserId = BlogAppContextFactory.UserAId,
                Header = newHeader,
            },
            CancellationToken.None
        ));
    }
}
