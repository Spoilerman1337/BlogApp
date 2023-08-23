using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Posts.Commands.DetachTags;
using BlogApp.Domain.Entites;
using BlogApp.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.Posts.Commands;

public class DetachTagsCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DetachTagsCommandHandlerTests_Success()
    {
        //Arrange
        var handler = new DetachTagsCommandHandler(Context);
        var postId = BlogAppContextFactory.ToBeUpdatedPostId;

        var tagA = Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF");
        var tagB = Guid.Parse("79121046-24BD-4518-813F-79878A48AC73");

        var tagIds = new List<Guid>
        {
            tagA, tagB
        };
        var post = await Context.Posts.SingleOrDefaultAsync(c => c.Id == postId);

        //Act
        await handler.Handle(
            new DetachTagsCommand
            {
                Id = postId,
                TagId = tagIds
            },
            CancellationToken.None
        );

        //Assert
        Assert.Empty(post!.Tags);
    }

    [Fact]
    public async Task AttachTagsCommandHandlerTests_NoTagFail()
    {
        //Arrange
        var handler = new DetachTagsCommandHandler(Context);
        var postId = BlogAppContextFactory.ToBeUpdatedPostId;

        var tag = Guid.NewGuid();

        var tagIds = new List<Guid>
        {
            tag
        };
        var post = new Post { Id = postId };

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new DetachTagsCommand
            {
                Id = postId,
                TagId = tagIds
            },
            CancellationToken.None
        ));
    }

    [Fact]
    public async Task AttachTagsCommandHandlerTests_NoPostFail()
    {
        //Arrange
        var handler = new DetachTagsCommandHandler(Context);
        var postId = Guid.NewGuid();

        var tag = Guid.Parse("79121046-24BD-4518-813F-79878A48AC73");

        var tagIds = new List<Guid>
        {
            tag
        };
        var post = await Context.Posts.SingleOrDefaultAsync(c => c.Id == postId);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new DetachTagsCommand
            {
                Id = postId,
                TagId = tagIds
            },
            CancellationToken.None
        ));
    }
}