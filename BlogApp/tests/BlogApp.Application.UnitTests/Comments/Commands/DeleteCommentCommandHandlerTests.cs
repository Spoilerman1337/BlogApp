﻿using BlogApp.Application.Comments.Commands.DeleteComment;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Commands;

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
                Id = BlogAppContextFactory.ToBeUpdatedCommentId
            },
            CancellationToken.None
        );

        //Assert
        (await Context.Comments.SingleOrDefaultAsync(c => c.Id == BlogAppContextFactory.ToBeUpdatedCommentId)).Should().BeNull();
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