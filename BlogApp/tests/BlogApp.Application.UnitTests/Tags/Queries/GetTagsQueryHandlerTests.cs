using AutoMapper;
using BlogApp.Application.Tags.Queries.GetTags;
using BlogApp.Application.Tags.Queries.GetTags.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Application.UnitTests.Tags.Queries;

[Collection("QueryCollection")]
public class GetTagsQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetTagsQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetTagsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetTagsQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetTagsQuery
            {
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetTagsDto>>();
    }

    [Fact]
    public async Task GetTagsQueryHandler_NoTagsFail()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<BlogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new BlogDbContext(options);
        var handler = new GetTagsQueryHandler(context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetTagsQuery
            {
                BypassCache = true
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetTagsDto>>();
    }
}
