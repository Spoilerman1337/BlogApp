﻿using AutoMapper;
using BlogApp.Application.Comments.Queries.GetCommentsFromPost;
using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Queries;

[Collection("QueryCollection")]
public class GetCommentsFromPostQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetCommentsFromPostQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }
    [Fact]
    public async Task GetCommentsFromPostQueryHandler_Success()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA"),
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsFromPostDto>>();
    }

    [Fact]
    public async Task GetCommentsFromPostQueryHandler_NoCommentsInPostFail()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"),
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetCommentsFromPostDto>>();
    }

    [Fact]
    public async Task GetCommentsFromPostQueryHandler_NoPostFail()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context, _mapper);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.NewGuid(),
                BypassCache = true
            },
            CancellationToken.None
        ));
    }
}
