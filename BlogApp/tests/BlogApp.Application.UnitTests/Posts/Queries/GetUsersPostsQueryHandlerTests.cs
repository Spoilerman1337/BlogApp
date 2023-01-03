using AutoMapper;
using BlogApp.Application.Posts.Queries.GetUsersPosts;
using BlogApp.Application.Posts.Queries.GetUsersPosts.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Posts.Queries;

[Collection("QueryCollection")]
public class GetUsersPostsQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersPostsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUsersPostsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersPostsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersPostsQuery
            {
                UserId = BlogAppContextFactory.UserAId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersPostsDto>>();
    }

    [Fact]
    public async Task GetUsersPostsQueryHandlerDateFilter_SuccessNotFound()
    {
        //Arrange
        var handler = new GetUsersPostsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersPostsQuery
            {
                UserId = BlogAppContextFactory.UserBId,
                From = DateTime.Parse("2008-11-01T19:35:00.0000000Z"),
                To = DateTime.Parse("2009-11-01T19:35:00.0000000Z")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersPostsDto>>();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUsersPostsQueryHandlerDateFilter_SuccessFound()
    {
        //Arrange
        var handler = new GetUsersPostsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersPostsQuery
            {
                UserId = BlogAppContextFactory.UserBId,
                From = DateTime.Now.AddYears(-1),
                To = DateTime.Now.AddYears(1)
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersPostsDto>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetUsersPostsQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetUsersPostsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersPostsQuery
            {
                UserId = BlogAppContextFactory.UserBId,
                Page = 0,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersPostsDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetUsersPostsQueryHandler_NoPostsFail()
    {
        //Arrange
        var handler = new GetUsersPostsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersPostsQuery
            {
                UserId = BlogAppContextFactory.NoPostUser
            },
            CancellationToken.None);

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetUsersPostsDto>>();
    }
}
