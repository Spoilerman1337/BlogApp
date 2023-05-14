using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Application.VotePosts.Queries.GetPostsVotes;
using BlogApp.Application.VotePosts.Queries.GetPostsVotes.Models;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.VotePosts.Queries;

[Collection("QueryCollection")]
public class GetPostsVotesQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetPostsVotesQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetPostsVotesQueryHandler_Success()
    {
        //Arrange
        var handler = new GetPostsVotesQueryHandler(_context);
        var postId = Guid.Parse("2A9C5C84-032D-49D6-B43B-D4028679B8D9");

        //Act
        var result = await handler.Handle(
            new GetPostsVotesQuery
            {
                PostId = postId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetPostsVotesDto>>();
    }

    [Fact]
    public async Task GetPostsVotesQueryHandler_NoPostThrows()
    {
        //Arrange
        var handler = new GetPostsVotesQueryHandler(_context);
        var postId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetPostsVotesQuery
            {
                PostId = postId
            },
            CancellationToken.None
        ));
    }
}
