using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Application.VoteComments.Commands.UpvoteComment;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.VoteComments.Commands;

public class UpvoteCommentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpvoteCommentCommandHandler_Success()
    {
        //Arrange
        var handler = new UpvoteCommentCommandHandler(Context);
        var vote = true;
        var commentId = Guid.Parse("0401EAD8-CB5C-4BCF-8ABF-7F63FCCF3155");
        var userId = BlogAppContextFactory.UserAId;

        //Act
        var votePostStatus = await handler.Handle(
            new UpvoteCommentCommand
            {
                CommentId = commentId,
                UserId = userId,
                IsUpvoted = vote
            },
            CancellationToken.None
        );

        //Assert
        (await Context.VoteComments.SingleOrDefaultAsync(c => c.IsUpvoted == votePostStatus && c.CommentId == commentId && c.UserId == userId)).Should().NotBeNull();
    }

    [Fact]
    public async Task UpvoteCommentCommandHandler_NoCommentThrows()
    {
        //Arrange
        var handler = new UpvoteCommentCommandHandler(Context);
        var vote = true;
        var commentId = Guid.NewGuid();
        var userId = BlogAppContextFactory.UserAId;

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpvoteCommentCommand
            {
                CommentId = commentId,
                UserId = userId,
                IsUpvoted = vote
            },
            CancellationToken.None
        ));
    }
}
