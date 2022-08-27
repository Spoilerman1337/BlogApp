﻿using AutoMapper;
using BlogApp.Application.Comments.Queries.GetComment;
using BlogApp.Application.Comments.Queries.GetComment.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Queries;

[Collection("QueryCollection")]
public class GetCommentQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetCommentQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetCommentQueryHandler_Success()
    {
        //Arrange
        var handler = new GetCommentQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentQuery
            {
                Id = Guid.Parse("0401EAD8-CB5C-4BCF-8ABF-7F63FCCF3155"),
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetCommentDto>();
    }

    [Fact]
    public async Task GetCommentQueryHandler_Fail()
    {
        //Arrange
        var handler = new GetCommentQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetCommentQuery
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeNull();
    }
}
