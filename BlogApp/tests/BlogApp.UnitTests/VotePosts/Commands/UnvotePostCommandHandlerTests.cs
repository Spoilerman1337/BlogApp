﻿using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.VotePosts.Commands.UnvotePost;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.VotePosts.Commands;

public class UnvotePostCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UnvotePostCommandHandler_Success()
    {
        //Arrange
        var handler = new UnvotePostCommandHandler(Context);
        var postId = BlogAppContextFactory.ToBeUpdatedPostId;
        var userId = BlogAppContextFactory.NoPostUser;

        //Act
        await handler.Handle(
            new UnvotePostCommand
            {
                PostId = postId,
                UserId = userId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.VotePosts.SingleOrDefaultAsync(c => c.PostId == postId && c.UserId == userId)).Should().BeNull();
    }

    [Fact]
    public async Task UnvotePostCommandHandler_NotFoundThrows()
    {
        //Arrange
        var handler = new UnvotePostCommandHandler(Context);
        var postId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UnvotePostCommand
            {
                PostId = postId,
                UserId = userId
            },
            CancellationToken.None
        ));
    }
}