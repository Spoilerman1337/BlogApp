﻿using BlogApp.Application.Comments.Commands.CreateComment;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.Comments.Commands;

public class CreateCommentCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateCommentCommandHandler_Success()
    {
        //Arrange
        var handler = new CreateCommentCommandHandler(Context);
        var text = "Quisque tristique dictum felis, a.";

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
}