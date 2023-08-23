using BlogApp.Application.Posts.Commands.CreatePost;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.Posts.Commands;

public class CreatePostCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreatePostCommandHandler_Success()
    {
        //Arrange
        var handler = new CreatePostCommandHandler(Context);
        var text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
        var header = "Integer nec consectetur tellus, nec dictum sem. In.";

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
        (await Context.Posts.SingleOrDefaultAsync(c => c.Id == postId && c.Text == text && c.Header == header)).Should()
            .NotBeNull();
    }
}