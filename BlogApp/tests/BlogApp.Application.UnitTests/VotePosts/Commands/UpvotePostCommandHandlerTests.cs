using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Application.VotePosts.Commands.UpvotePost;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.VotePosts.Commands;

public class UpvotePostCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpvotePostCommandHandler_Success()
    {
        //Arrange
        var handler = new UpvotePostCommandHandler(Context);
        var vote = true;
        var postId = Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA");
        var userId = BlogAppContextFactory.UserAId;

        //Act
        var votePostStatus = await handler.Handle(
            new UpvotePostCommand
            {
                PostId = postId,
                UserId = userId,
                IsUpvoted = vote
            },
            CancellationToken.None
        );

        //Assert
        (await Context.VotePosts.SingleOrDefaultAsync(c => c.IsUpvoted == votePostStatus && c.PostId == postId && c.UserId == userId)).Should().NotBeNull();
    }

    [Fact]
    public async Task UpvotePostCommandHandler_NoPostThrows()
    {
        //Arrange
        var handler = new UpvotePostCommandHandler(Context);
        var vote = true;
        var postId = Guid.NewGuid();
        var userId = BlogAppContextFactory.UserAId;

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpvotePostCommand
            {
                PostId = postId,
                UserId = userId,
                IsUpvoted = vote
            },
            CancellationToken.None
        ));
    }
}
