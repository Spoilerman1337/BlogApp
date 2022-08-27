using AutoMapper;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Posts.Queries.GetPostsByTag;
using BlogApp.Application.Posts.Queries.GetPostsByTag.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Posts.Queries;

[Collection("QueryCollection")]
public class GetPostsByTagQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetPostsByTagQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetPostsByTagQueryHandler_Success()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = Guid.Parse("79121046-24BD-4518-813F-79878A48AC73"),
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsByTagDto>>();
    }

    [Fact]
    public async Task GetPostsByTagQueryHandler_NoPostsFail()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = BlogAppContextFactory.ToBeUpdatedTagId,
                BypassCache = true
            },
            CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsByTagDto>>();
    }

    [Fact]
    public async Task GetPostsByTagQueryHandler_NoTagThrows()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context, _mapper);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = Guid.NewGuid(),
                BypassCache = true
            },
            CancellationToken.None
        ));
    }
}
