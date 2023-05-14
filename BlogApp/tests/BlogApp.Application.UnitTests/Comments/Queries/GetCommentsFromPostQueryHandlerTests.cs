using BlogApp.Application.Comments.Queries.GetCommentsFromPost;
using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Queries;

[Collection("QueryCollection")]
public class GetCommentsFromPostQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetCommentsFromPostQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetCommentsFromPostQueryHandler_Success()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsFromPostDto>>();
    }

    [Fact]
    public async Task GetCommentsFromPostQueryHandlerDateFilter_SuccessNotFound()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA"),
                From = DateTime.Parse("2008-11-01T19:35:00.0000000Z"),
                To = DateTime.Parse("2009-11-01T19:35:00.0000000Z")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsFromPostDto>>();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetCommentsFromPostQueryHandlerDateFilter_SuccessFound()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA"),
                From = DateTime.Today.AddYears(-1),
                To = DateTime.Today.AddYears(1)
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsFromPostDto>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetCommentsFromPostQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA"),
                Page = 1,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsFromPostDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetCommentsFromPostQueryHandler_NoCommentsInPostFail()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetCommentsFromPostDto>>();
    }

    [Fact]
    public async Task GetCommentsFromPostQueryHandler_NoPostFail()
    {
        //Arrange
        var handler = new GetCommentsFromPostQueryHandler(_context);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetCommentsFromPostQuery
            {
                PostId = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }
}
