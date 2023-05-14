using BlogApp.Application.Posts.Queries.GetPosts;
using BlogApp.Application.Posts.Queries.GetPosts.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Posts.Queries;

[Collection("QueryCollection")]
public class GetPostsQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetPostsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetPostsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetPostsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsDto>>();
    }

    [Fact]
    public async Task GetPostsQueryHandlerDateFilter_SuccessNotFound()
    {
        //Arrange
        var handler = new GetPostsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsQuery
            {
                From = DateTime.Parse("2008-11-01T19:35:00.0000000Z"),
                To = DateTime.Parse("2009-11-01T19:35:00.0000000Z")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsDto>>();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetPostsQueryHandlerDateFilter_SuccessFound()
    {
        //Arrange
        var handler = new GetPostsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsQuery
            {
                From = DateTime.Now.AddYears(-1),
                To = DateTime.Now.AddYears(1)
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsDto>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetPostsQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetPostsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsQuery
            {
                Page = 0,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetPostsQueryHandler_NoPostsFail()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new BlogDbContext(options);
        var handler = new GetPostsQueryHandler(context);

        //Act
        var result = await handler.Handle(
            new GetPostsQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetPostsDto>>();
    }
}
