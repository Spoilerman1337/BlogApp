using BlogApp.Application.Tags.Commands.CreateTag;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.Tags.Commands;

public class CreateTagCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateTagCommandHandler_Success()
    {
        //Arrange
        var handler = new CreateTagCommandHandler(Context);
        var tagName = "Etiam";

        //Act
        var tagId = await handler.Handle(
            new CreateTagCommand
            {
                TagName = tagName
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Tags.SingleOrDefaultAsync(c => c.Id == tagId && c.TagName == tagName)).Should().NotBeNull();
    }
}