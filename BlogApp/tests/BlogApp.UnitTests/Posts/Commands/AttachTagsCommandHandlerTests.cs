using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Posts.Commands.AttachTags;
using BlogApp.Domain.Entites;
using BlogApp.UnitTests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.Posts.Commands;

public class AttachTagsCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task AttachTagsCommandHandlerTests_Success()
    {
        //Arrange
        var handler = new AttachTagsCommandHandler(Context);
        var postId = BlogAppContextFactory.ToBeUpdatedPostId;

        var tagA = Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF");
        var tagB = Guid.Parse("79121046-24BD-4518-813F-79878A48AC73");

        var tagIds = new List<Guid>
        {
            tagB
        };
        var post = await Context.Posts.SingleOrDefaultAsync(c => c.Id == postId);

        //Act
        await handler.Handle(
            new AttachTagsCommand
            {
                Id = postId,
                TagId = tagIds
            },
            CancellationToken.None
        );

        //Assert
        Assert.Contains(tagA, post!.Tags.Select(c => c.Id));
        Assert.Contains(tagB, post!.Tags.Select(c => c.Id));
    }

    [Fact]
    public async Task AttachTagsCommandHandlerTests_NoTagFail()
    {
        //Arrange
        var handler = new AttachTagsCommandHandler(Context);
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
            new AttachTagsCommand
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
        var handler = new AttachTagsCommandHandler(Context);
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
            new AttachTagsCommand
            {
                Id = postId,
                TagId = tagIds
            },
            CancellationToken.None
        ));
    }
}