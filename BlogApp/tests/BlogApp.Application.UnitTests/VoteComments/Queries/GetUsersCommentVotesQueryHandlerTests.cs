using BlogApp.Application.UnitTests.Common;
using BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes;
using BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes.Models;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.VoteComments.Queries;

[Collection("QueryCollection")]
public class GetUsersCommentVotesQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetUsersCommentVotesQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetUsersCommentVotesQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersCommentVotesQueryHandler(_context);
        var userId = BlogAppContextFactory.NoPostUser;

        //Act
        var result = await handler.Handle(
            new GetUsersCommentVotesQuery
            {
                UserId = userId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersCommentVotesDto>>();
    }
}
