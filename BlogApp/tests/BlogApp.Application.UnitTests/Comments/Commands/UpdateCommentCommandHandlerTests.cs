using BlogApp.Application.Comments.Commands.UpdateComment;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Commands;

public class UpdateCommentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateCommentCommandHandler_Success()
    {
        //Arrange
        var handler = new UpdateCommentCommandHandler(Context);
        string newText = "Donec vitae consectetur arcu. Pellentesque.";

        //Act
        await handler.Handle(
            new UpdateCommentCommand
            {
                Id = BlogAppContextFactory.ToBeUpdatedCommentId,
                Text = newText,
                UserId = BlogAppContextFactory.UserBId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Comments.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeUpdatedCommentId && c.Text == newText)).Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateCommentCommandHandler_WrongUserShouldThrow()
    {
        //Arrange
        var handler = new UpdateCommentCommandHandler(Context);
        string newText = "Donec vitae consectetur arcu. Pellentesque.";

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpdateCommentCommand
            {
                Id = BlogAppContextFactory.ToBeUpdatedCommentId,
                Text = newText,
                UserId = BlogAppContextFactory.UserAId
            },
            CancellationToken.None
        ));
    }

    [Fact]
    public async Task UpdateCommentCommandHandler_WrongIdShouldThrow()
    {
        //Arrange
        var handler = new UpdateCommentCommandHandler(Context);
        string newText = "Donec vitae consectetur arcu. Pellentesque.";

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpdateCommentCommand
            {
                Id = Guid.NewGuid(),
                Text = newText,
                UserId = BlogAppContextFactory.UserAId
            },
            CancellationToken.None
        ));
    }
}
