using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Roles.Queries.GetUsersRole;
using BlogApp.Auth.Application.Roles.Queries.GetUsersRole.Models;
using BlogApp.Auth.Application.UnitTests.Common;
using BlogApp.Auth.Domain.Entities;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.Auth.Application.UnitTests.Roles.Queries;

[Collection("QueryCollection")]
public class GetUsersRoleQueryHandlerTests
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly IMapper _mapper;

    public GetUsersRoleQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _userManager = fixture._userManager;
        _roleManager = fixture._roleManager;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUsersRoleQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersRoleQueryHandler(_userManager, _roleManager, _mapper);
        var userId = Guid.Parse("FDAE39CF-B55E-47BC-9904-00DF3CFD6FAD");

        //Act
        var result = await handler.Handle(
            new GetUsersRoleQuery
            {
                UserId = userId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetUsersRoleDto>();
    }

    [Fact]
    public async Task GetUsersRoleQueryHandler_NoRolesFail()
    {
        //Arrange
        var handler = new GetUsersRoleQueryHandler(_userManager, _roleManager, _mapper);
        var userId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetUsersRoleQuery
            {
                UserId = userId
            },
            CancellationToken.None
        ));
    }
}
