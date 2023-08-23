using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Tags.Queries.GetPostsTags;
using BlogApp.Application.Tags.Queries.GetPostsTags.Models;
using BlogApp.Infrastructure.Persistance;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Xunit;

namespace BlogApp.UnitTests.Tags.Queries;

[Collection("QueryCollection")]
public class GetPostsTagsQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetPostsTagsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetPostsTagsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetPostsTagsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsTagsQuery
            {
                PostId = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsTagsDto>>();
    }

    [Fact]
    public async Task GetPostsTagsQueryHandler_NoPostThrows()
    {
        //Arrange
        var handler = new GetPostsTagsQueryHandler(_context);

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetPostsTagsQuery
            {
                PostId = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }

    [Fact]
    public async Task GetPostsTagsQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetPostsTagsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsTagsQuery
            {
                PostId = BlogAppContextFactory.ToBeDeletedPostId,
                Page = 0,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsTagsDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetPostsTagsQueryHandler_NoTagsFail()
    {
        //Arrange
        var handler = new GetPostsTagsQueryHandler(_context);

        //Act
        var result = await handler.Handle(
            new GetPostsTagsQuery
            {
                PostId = Guid.Parse("D30526A7-E44C-4163-B8A7-E0452C7E12FA")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetPostsTagsDto>>();
    }
}