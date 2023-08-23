using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Posts.Queries.GetPostsByTag;
using BlogApp.Application.Posts.Queries.GetPostsByTag.Models;
using BlogApp.Infrastructure.Persistance;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Xunit;

namespace BlogApp.UnitTests.Posts.Queries;

[Collection("QueryCollection")]
public class GetPostsByTagQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetPostsByTagQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetPostsByTagQueryHandler_Success()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = Guid.Parse("79121046-24BD-4518-813F-79878A48AC73")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsByTagDto>>();
    }

    [Fact]
    public async Task GetPostsByTagQueryHandlerDateFilter_SuccessNotFound()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF"),
                From = DateTime.Parse("2008-11-01T19:35:00.0000000Z"),
                To = DateTime.Parse("2009-11-01T19:35:00.0000000Z")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsByTagDto>>();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetPostsByTagQueryHandlerDateFilter_SuccessFound()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF"),
                From = DateTime.Now.AddYears(-1),
                To = DateTime.Now.AddYears(1)
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsByTagDto>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetPostsByTagQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF"),
                Page = 0,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsByTagDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetPostsByTagQueryHandler_NoPostsFail()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = BlogAppContextFactory.ToBeUpdatedTagId
            },
            CancellationToken.None);

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetPostsByTagDto>>();
    }

    [Fact]
    public async Task GetPostsByTagQueryHandler_NoTagThrows()
    {
        //Arrange
        var handler = new GetPostsByTagQueryHandler(_context);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetPostsByTagQuery
            {
                TagId = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }
}