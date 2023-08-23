using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Users.Queries.GetUsersByRole;
using BlogApp.Auth.Application.Users.Queries.GetUsersByRole.Models;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace BlogApp.Auth.UnitTests.Users.Queries;

[Collection("QueryCollection")]
public class GetUsersByRoleQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly RoleManager<RoleEntity> _roleManager;
    private readonly UserManager<UserEntity> _userManager;

    public GetUsersByRoleQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _userManager = fixture._userManager;
        _roleManager = fixture._roleManager;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUsersByRoleQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersByRoleQueryHandler(_userManager, _roleManager, _mapper);
        var roleId = BlogAppContextFactory.ToBeUpdatedRoleId;

        //Act
        var result = await handler.Handle(
            new GetUsersByRoleQuery
            {
                RoleId = roleId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersByRoleDto>>();
    }

    [Fact]
    public async Task GetUsersByRoleQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetUsersByRoleQueryHandler(_userManager, _roleManager, _mapper);
        var roleId = BlogAppContextFactory.ToBeUpdatedRoleId;

        //Act
        var result = await handler.Handle(
            new GetUsersByRoleQuery
            {
                RoleId = roleId,
                Page = 0,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersByRoleDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetUsersByRoleQueryHandler_NoUsersFail()
    {
        //Arrange
        var handler = new GetUsersByRoleQueryHandler(_userManager, _roleManager, _mapper);
        var roleId = Guid.Parse("5C29FCAB-1AAD-4EDD-8D75-3770D98D651D");

        //Act
        var result = await handler.Handle(
            new GetUsersByRoleQuery
            {
                RoleId = roleId
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersByRoleDto>>();
    }

    [Fact]
    public async Task GetUsersByRoleQueryHandler_NoRoleThrows()
    {
        //Arrange
        var handler = new GetUsersByRoleQueryHandler(_userManager, _roleManager, _mapper);
        var roleId = Guid.NewGuid();

        //Act

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(
            new GetUsersByRoleQuery
            {
                RoleId = roleId
            },
            CancellationToken.None
        ));
    }
}