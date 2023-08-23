using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Users.Queries.GetUserByName;
using BlogApp.Auth.Application.Users.Queries.GetUserByName.Models;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace BlogApp.Auth.UnitTests.Users.Queries;

[Collection("QueryCollection")]
public class GetUserByNameQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

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