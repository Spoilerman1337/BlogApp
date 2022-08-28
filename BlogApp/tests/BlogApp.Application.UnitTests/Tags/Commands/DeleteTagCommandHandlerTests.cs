using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Tags.Commands.DeleteTag;
using BlogApp.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Tags.Commands;

public class DeleteTagCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteTagCommandHandler_Success()
    {
        //Arrange
        var handler = new DeleteTagCommandHandler(Context);

        //Act
        await handler.Handle(
            new DeleteTagCommand
            {
                Id = BlogAppContextFactory.ToBeDeletedTagId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Tags.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeDeletedTagId)).Should().BeNull();
    }

    [Fact]
    public async Task DeleteTagCommandHandler_WrongIdShouldThrow()
    {
        //Arrange
        var handler = new DeleteTagCommandHandler(Context);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new DeleteTagCommand
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }
}
