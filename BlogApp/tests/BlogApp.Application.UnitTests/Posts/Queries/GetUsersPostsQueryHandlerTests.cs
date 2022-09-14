using AutoMapper;
using BlogApp.Application.Common.Exceptions;
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
