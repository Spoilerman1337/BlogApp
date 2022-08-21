using BlogApp.Application.Posts.Commands.CreatePost;
using BlogApp.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Posts.Commands;

public class CreatePostCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreatePostCommandHandler_Success()
    {
        //Arrange
        var handler = new CreatePostCommandHandler(Context);
        string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
        string header = "Integer nec consectetur tellus, nec dictum sem. In.";

        //Act
        var postId = await handler.Handle(
            new CreatePostCommand
            {
                Text = text,
                UserId = BlogAppContextFactory.UserAId,
                Header = header
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Posts.SingleOrDefaultAsync(c => c.Id == postId && c.Text == text && c.Header == header)).Should().NotBeNull();
    }
}
