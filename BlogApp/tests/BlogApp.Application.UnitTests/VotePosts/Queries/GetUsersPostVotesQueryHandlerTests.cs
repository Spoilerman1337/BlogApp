using AutoMapper;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes;
using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes.Models;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.VotePosts.Queries;

[Collection("QueryCollection")]
public class GetUsersPostVotesQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersPostVotesQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUsersPostVotesQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersPostVotesQueryHandler(_context, _mapper);
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
