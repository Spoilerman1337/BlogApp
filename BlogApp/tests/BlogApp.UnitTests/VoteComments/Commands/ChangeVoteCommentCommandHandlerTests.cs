using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.VoteComments.Commands.ChangeVoteComment;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.VoteComments.Commands;

public class ChangeVoteCommentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task ChangeVoteCommentCommandHandler_Success()
    {
        //Arrange
        var handler = new ChangeVoteCommentCommandHandler(Context);
        var commentId = BlogAppContextFactory.ToBeUpdatedCommentId;
        var userId = BlogAppContextFactory.UserBId;
        var oldStatus = Context.VoteComments.First(c => c.CommentId == commentId && c.UserId == userId).IsUpvoted;

        //Act
        await handler.Handle(
            new ChangeVoteCommentCommand
            {
                CommentId = commentId,
                UserId = userId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.VoteComments.SingleOrDefaultAsync(c => c.CommentId == commentId && c.UserId == userId)).Should()
            .NotBeNull().And.NotBeEquivalentTo(oldStatus);
    }

    [Fact]
    public async Task ChangeVoteCommentCommandHandler_NotFoundThrows()
    {
        //Arrange
        var handler = new ChangeVoteCommentCommandHandler(Context);
        var commentId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new ChangeVoteCommentCommand
            {
                CommentId = commentId,
                UserId = userId
            },
            CancellationToken.None
        ));
    }
}