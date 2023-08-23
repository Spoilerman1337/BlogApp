using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes;
using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes.Models;
using BlogApp.Infrastructure.Persistance;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Xunit;

namespace BlogApp.UnitTests.VotePosts.Queries;

[Collection("QueryCollection")]
public class GetUsersPostVotesQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetUsersPostVotesQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetUsersPostVotesQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersPostVotesQueryHandler(_context);
        var userId = BlogAppContextFactory.NoPostUser;

        //Act
        var result = await handler.Handle(
            new GetUsersPostVotesQuery
            {
                UserId = userId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersPostVotesDto>>();
    }
}