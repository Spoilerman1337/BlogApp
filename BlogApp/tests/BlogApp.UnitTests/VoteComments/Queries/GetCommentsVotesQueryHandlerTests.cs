using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.VoteComments.Queries.GetCommentsVotes;
using BlogApp.Application.VoteComments.Queries.GetCommentsVotes.Models;
using BlogApp.Infrastructure.Persistance;
using BlogApp.UnitTests.Common;
using FluentAssertions;
using Xunit;

namespace BlogApp.UnitTests.VoteComments.Queries;

[Collection("QueryCollection")]
public class GetCommentsVotesQueryHandlerTests
{
    private readonly BlogDbContext _context;

    public GetCommentsVotesQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
    }

    [Fact]
    public async Task GetCommentsVotesQueryHandler_Success()
    {
        //Arrange
        var handler = new GetCommentsVotesQueryHandler(_context);
        var commentId = Guid.Parse("8BAB8E58-5FB2-4E92-AE7F-643DF6D3D2A6");

        //Act
        var result = await handler.Handle(
            new GetCommentsVotesQuery
            {
                CommentId = commentId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetCommentsVotesDto>>();
    }

    [Fact]
    public async Task GetCommentsVotesQueryHandler_NoCommentThrows()
    {
        //Arrange
        var handler = new GetCommentsVotesQueryHandler(_context);
        var commentId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetCommentsVotesQuery
            {
                CommentId = commentId
            },
            CancellationToken.None
        ));
    }
}