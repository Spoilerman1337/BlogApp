using System;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Users.Queries.GetUser;
using BlogApp.Auth.Application.Users.Queries.GetUser.Models;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace BlogApp.Auth.UnitTests.Users.Queries;

[Collection("QueryCollection")]
public class GetUserQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    public GetUserQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _userManager = fixture._userManager;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUserQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUserQueryHandler(_userManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUserQuery
            {
                Id = Guid.Parse("FDAE39CF-B55E-47BC-9904-00DF3CFD6FAD")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetUserDto>();
    }

    [Fact]
    public async Task GetUserQueryHandler_Fail()
    {
        //Arrange
        var handler = new GetUserQueryHandler(_userManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUserQuery
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeNull();
    }
}