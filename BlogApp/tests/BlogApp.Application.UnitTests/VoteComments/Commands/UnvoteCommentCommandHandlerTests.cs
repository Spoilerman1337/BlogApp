using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Application.VoteComments.Commands.UnvoteComment;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.VoteComments.Commands;

public class UnvoteCommentCommandHandlerTests : TestCommandBase
{

    [Fact]
    public async Task UnvoteCommentCommandHandler_Success()
    {
        //Arrange
        var handler = new UnvoteCommentCommandHandler(Context);
        var commentId = BlogAppContextFactory.ToBeUpdatedCommentId;
        var userId = BlogAppContextFactory.NoPostUser;

        //Act
        await handler.Handle(
            new UnvoteCommentCommand
            {
                CommentId = commentId,
                UserId = userId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.VoteComments.SingleOrDefaultAsync(c => c.CommentId == commentId && c.UserId == userId)).Should().BeNull();
    }

    [Fact]
    public async Task UnvoteCommentCommandHandler_NotFoundThrows()
    {
        //Arrange
        var handler = new UnvoteCommentCommandHandler(Context);
        var commentId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UnvoteCommentCommand
            {
                CommentId = commentId,
                UserId = userId
            },
            CancellationToken.None
        ));
    }
}
