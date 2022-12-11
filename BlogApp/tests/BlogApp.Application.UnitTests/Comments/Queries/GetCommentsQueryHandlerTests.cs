﻿using AutoMapper;
using BlogApp.Application.Comments.Queries.GetComments;
using BlogApp.Application.Comments.Queries.GetComments.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Queries;

[Collection("QueryCollection")]
public class GetCommentsQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetCommentsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetCommentsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentsQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsDto>>();
    }

    [Fact]
    public async Task GetCommentsQueryHandlerDateFilter_SuccessNotFound()
    {
        //Arrange
        var handler = new GetCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentsQuery
            {
                From = DateTime.Parse("2008-11-01T19:35:00.0000000Z"),
                To = DateTime.Parse("2009-11-01T19:35:00.0000000Z")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsDto>>();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetCommentsQueryHandlerDateFilter_SuccessFound()
    {
        //Arrange
        var handler = new GetCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentsQuery
            {
                From = DateTime.Today.AddDays(-1),
                To = DateTime.Today.AddDays(1)
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsDto>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetCommentsQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentsQuery
            {
                Page = 1,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetCommentsQueryHandler_NoCommentsFail()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new BlogDbContext(options);
        var handler = new GetCommentsQueryHandler(context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentsQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsDto>>();
    }
}
