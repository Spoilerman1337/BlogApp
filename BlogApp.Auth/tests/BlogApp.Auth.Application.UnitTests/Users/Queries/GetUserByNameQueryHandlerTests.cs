using BlogApp.Auth.Application.UnitTests.Common;
using BlogApp.Auth.Application.Users.Queries.GetUserByName;
using BlogApp.Auth.Application.Users.Queries.GetUserByName.Models;
using BlogApp.Auth.Domain.Entities;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.Auth.Application.UnitTests.Users.Queries;

[Collection("QueryCollection")]
public class GetUserByNameQueryHandlerTests
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetUserByNameQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _userManager = fixture._userManager;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUserByNameQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUserByNameQueryHandler(_userManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUserByNameQuery
            {
                UserName = "Sit"
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetUserByNameDto>();
    }

    [Fact]
    public async Task GetUserByNameQueryHandler_Fail()
    {
        //Arrange
        var handler = new GetUserByNameQueryHandler(_userManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUserByNameQuery
            {
                UserName = "hldhasld"
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeNull();
    }
}
