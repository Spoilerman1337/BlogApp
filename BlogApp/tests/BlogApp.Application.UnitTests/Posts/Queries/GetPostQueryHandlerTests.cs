using AutoMapper;
using BlogApp.Application.Posts.Queries.GetPost;
using BlogApp.Application.Posts.Queries.GetPost.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Posts.Queries;

[Collection("QueryCollection")]
public class GetPostQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetPostQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetPostQueryHandler_Success()
    {
        //Arrange
        var handler = new GetPostQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetPostQuery
            {
                Id = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9"),
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetPostDto>();
    }

    [Fact]
    public async Task GetPostQueryHandler_Fail()
    {
        //Arrange
        var handler = new GetPostQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetPostQuery
            {
                Id = Guid.NewGuid(),
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeNull();
    }
}
