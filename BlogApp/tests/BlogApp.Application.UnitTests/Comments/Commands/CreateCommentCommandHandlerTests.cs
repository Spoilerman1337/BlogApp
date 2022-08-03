using BlogApp.Application.Comments.Commands.CreateComment;
using BlogApp.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Commands;

public class CreateCommentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateCommentCommandHandler_Success()
    {
        //Arrange
        var handler = new CreateCommentCommandHandler(Context);
        string text = "Quisque tristique dictum felis, a.";

        //Act
        var commentId = await handler.Handle(
            new CreateCommentCommand
            {
                Text = text,
                PostId = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"),
                UserId = BlogAppContextFactory.UserAId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Comments.SingleOrDefaultAsync(c => c.Id == commentId && c.Text == text)).Should().NotBeNull();
    }

    [Fact]
    public async Task CreateCommentCommandHandler_NullTextShouldFail()
    {
        //Arrange
        var handler = new CreateCommentCommandHandler(Context);

        //Act

        //Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () => await handler.Handle(
            new CreateCommentCommand
            {
                Text = null,
                PostId = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"),
                UserId = BlogAppContextFactory.UserAId
            },
            CancellationToken.None
        ));
    }
}
