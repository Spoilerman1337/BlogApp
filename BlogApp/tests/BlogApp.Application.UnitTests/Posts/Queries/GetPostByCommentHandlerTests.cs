﻿using AutoMapper;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Posts.Queries.GetPostByComment;
using BlogApp.Application.Posts.Queries.GetPostByComment.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Posts.Queries;

[Collection("QueryCollection")]
public class GetPostByCommentHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetPostByCommentHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetPostsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetPostByCommentQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetPostByCommentQuery
            {
                CommentId = Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6"),
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetPostByCommentDto>();
    }

    [Fact]
    public async Task GetPostsQueryHandler_NoPostsOrNoSuchCommentThrows()
    {
        //Arrange
        var handler = new GetPostByCommentQueryHandler(_context, _mapper);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetPostByCommentQuery
            {
                CommentId = Guid.NewGuid(),
                BypassCache = true
            },
            CancellationToken.None
        ));
    }
}