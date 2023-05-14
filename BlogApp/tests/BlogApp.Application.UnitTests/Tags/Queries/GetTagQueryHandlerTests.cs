using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Tags.Queries.GetTag;
using BlogApp.Application.Tags.Queries.GetTag.Models;
using BlogApp.Application.UnitTests.Common;
using BlogApp.Infrastructure.Persistance;
using FluentAssertions;
using MapsterMapper;
using Xunit;

namespace BlogApp.Application.UnitTests.Tags.Queries;

[Collection("QueryCollection")]
public class GetTagQueryHandlerTests
{
    private readonly BlogDbContext _context;
    private readonly IMapper _mapper;

    public GetTagQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _context = fixture._context;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetTagQueryHandler_Success()
    {
        //Arrange
        var handler = new GetTagQueryHandler(_context, _mapper);

        //Act
        var result = await handler.Handle(
            new GetTagQuery
            {
                Id = Guid.Parse("3E07C1D3-01B8-4CC1-B32D-0E5813A0D2FF")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetTagDto>();
    }

    [Fact]
    public async Task GetTagQueryHandler_Throws()
    {
        //Arrange
        var handler = new GetTagQueryHandler(_context, _mapper);

        //Act

        //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.Handle(
            new GetTagQuery
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        ));
    }
}
