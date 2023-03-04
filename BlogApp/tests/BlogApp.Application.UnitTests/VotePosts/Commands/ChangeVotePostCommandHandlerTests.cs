using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Application.VotePosts.Commands.ChangeVotePost;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.VotePosts.Commands;

public class ChangeVotePostCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task ChangeVotePostCommandHandler_Success()
    {
        //Arrange
        var handler = new ChangeVotePostCommandHandler(Context);
        var postId = BlogAppContextFactory.ToBeUpdatedPostId;
        var userId = BlogAppContextFactory.UserBId;
        var oldStatus = Context.VotePosts.First(c => c.PostId == postId && c.UserId == userId).IsUpvoted;

        //Act
        await handler.Handle(
            new ChangeVotePostCommand
            {
                PostId = postId,
                UserId = userId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.VotePosts.SingleOrDefaultAsync(c => c.PostId == postId && c.UserId == userId)).Should().NotBeNull().And.NotBeEquivalentTo(oldStatus);
    }

    [Fact]
    public async Task ChangeVotePostCommandHandler_NotFoundThrows()
    {
        //Arrange
        var handler = new ChangeVotePostCommandHandler(Context);
        var postId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new ChangeVotePostCommand
            {
                PostId = postId,
                UserId = userId
            },
            CancellationToken.None
        ));
    }
}
