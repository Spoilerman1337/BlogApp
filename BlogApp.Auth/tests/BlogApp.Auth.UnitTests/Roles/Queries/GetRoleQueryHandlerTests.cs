using System;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Roles.Queries.GetRole;
using BlogApp.Auth.Application.Roles.Queries.GetRole.Models;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.UnitTests.Common;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace BlogApp.Auth.UnitTests.Roles.Queries;

[Collection("QueryCollection")]
public class GetRoleQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly RoleManager<RoleEntity> _roleManager;

    public GetRoleQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _roleManager = fixture._roleManager;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetRoleQueryHandler_Success()
    {
        //Arrange
        var handler = new GetRoleQueryHandler(_roleManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetRoleQuery
            {
                Id = Guid.Parse("5C29FCAB-1AAD-4EDD-8D75-3770D98D651D")
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetRoleDto>();
    }

    [Fact]
    public async Task GetRoleQueryHandler_Fail()
    {
        //Arrange
        var handler = new GetRoleQueryHandler(_roleManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetRoleQuery
            {
                Id = Guid.NewGuid()
            },
            CancellationToken.None
        );

        //Assert
        result.Should().BeNull();
    }
}