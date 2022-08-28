using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Tags.Commands.UpdateTag;
using BlogApp.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Tags.Commands;

public class UpdateTagCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateTagCommandHandler_Success()
    {
        //Arrange
        var handler = new UpdateTagCommandHandler(Context);
        string newTagName = "Integer";

        //Act
        await handler.Handle(
            new UpdateTagCommand
            {
                Id = BlogAppContextFactory.ToBeUpdatedTagId,
                TagName = newTagName
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Tags.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeUpdatedTagId && c.TagName == newTagName)).Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateTagCommandHandler_WrongIdShouldThrow()
    {
        //Arrange
        var handler = new UpdateTagCommandHandler(Context);
        string newTagName = "Integer";

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new UpdateTagCommand
            {
                Id = Guid.NewGuid(),
                TagName = newTagName
            },
            CancellationToken.None
        ));
    }
}
