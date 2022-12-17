using AutoMapper;
using BlogApp.Application.Comments.Queries.GetUsersComments;
using BlogApp.Application.Comments.Queries.GetUsersComments.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Xunit;

namespace BlogApp.Application.UnitTests.Comments.Queries;

[Collection("QueryCollection")]
public class GetUsersCommentsQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersCommentsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = BlogAppContextFactory.UserAId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandlerDateFilter_SuccessNotFound()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = BlogAppContextFactory.UserAId,
                From = DateTime.Parse("2008-11-01T19:35:00.0000000Z"),
                To = DateTime.Parse("2009-11-01T19:35:00.0000000Z")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandlerDateFilter_SuccessFound()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = BlogAppContextFactory.UserBId,
                From = DateTime.Today.AddYears(-1),
                To = DateTime.Today.AddYears(1)
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = BlogAppContextFactory.UserBId,
                Page = 1,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
        result.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandler_NoPostsFail()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = BlogAppContextFactory.NoPostUser
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
    }

    [Fact]
    public async Task GetUsersCommentsQueryHandler_NoUserFail()
    {
        //Arrange
        var handler = new GetUsersCommentsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersCommentsQuery
            {
                UserId = Guid.NewGuid()
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetUsersCommentsDto>>();
    }
}
