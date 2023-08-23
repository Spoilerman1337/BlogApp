using BlogApp.Application.Comments.Commands.DeleteComment;
using BlogApp.Application.Common.Exceptions;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.Comments.Commands;

public class DeleteCommentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteCommentCommandHandler_Success()
    {
        //Arrange
        var handler = new DeleteCommentCommandHandler(Context);

        //Act
        await handler.Handle(
            new DeleteCommentCommand
            {
                Id = BlogAppContextFactory.ToBeDeletedCommentId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Comments.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeDeletedCommentId)).Should()
            .BeNull();
    }

    [Fact]
    public async Task DeleteCommentCommandHandler_WrongIdShouldThrow()
    {
        //Arrange
        var handler = new DeleteCommentCommandHandler(Context);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new DeleteCommentCommand
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }
}