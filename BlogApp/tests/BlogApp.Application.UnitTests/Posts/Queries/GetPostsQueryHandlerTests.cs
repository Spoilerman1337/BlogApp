using AutoMapper;
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
    private readonly IMapper _mapper;

    public GetPostsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetPostsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetPostsQueryHandler(_context, _mapper);

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
    public async Task GetPostsQueryHandler_NoPostsFail()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new BlogDbContext(options);
        var handler = new GetPostsQueryHandler(context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetPostsQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsDto>>();
    }
}
