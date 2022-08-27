using AutoMapper;
using BlogApp.Application.Comments.Queries.GetUsersComments;
using BlogApp.Application.Comments.Queries.GetUsersComments.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Queries;

[Collection("QueryCollection")]
public class GetUsersCommentsQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersCommentsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = BlogAppContextFactory.UserAId,
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandler_NoPostsFail()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = BlogAppContextFactory.NoPostUser
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandler_NoUserFail()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = Guid.NewGuid(),
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
    }
}
