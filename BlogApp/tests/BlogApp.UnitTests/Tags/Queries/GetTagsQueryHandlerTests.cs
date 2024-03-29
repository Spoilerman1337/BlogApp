﻿using BlogApp.Application.Tags.Queries.GetTags;
using BlogApp.Application.Tags.Queries.GetTags.Models;
using BlogApp.Infrastructure.Persistance;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.UnitTests.Tags.Queries;

[Collection("QueryCollection")]
public class GetTagsQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetTagsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetTagsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetTagsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetTagsQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetTagsDto>>();
    }

    [Fact]
    public async Task GetTagsQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetTagsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetTagsQuery
            {
                Page = 0,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetTagsDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetTagsQueryHandler_NoTagsFail()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new BlogDbContext(options);
        var handler = new GetTagsQueryHandler(context);

        //Act
        var result = await handler.Handle(
            new GetTagsQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetTagsDto>>();
    }
}