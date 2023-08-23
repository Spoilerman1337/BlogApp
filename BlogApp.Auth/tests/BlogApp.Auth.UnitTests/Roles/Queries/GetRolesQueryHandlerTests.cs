using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Roles.Queries.GetRoles;
using BlogApp.Auth.Application.Roles.Queries.GetRoles.Models;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using BlogApp.Auth.UnitTests.Common;
using BlogApp.Auth.UnitTests.Mocks;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Auth.UnitTests.Roles.Queries;

[Collection("QueryCollection")]
public class GetRolesQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly RoleManager<RoleEntity> _roleManager;

    public GetRolesQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _roleManager = fixture._roleManager;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetRolesQueryHandler_Success()
    {
        //Arrange
        var handler = new GetRolesQueryHandler(_roleManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetRolesQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetRolesDto>>();
    }

    [Fact]
    public async Task GetRolesQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetRolesQueryHandler(_roleManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetRolesQuery
            {
                Page = 0,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetRolesDto>>();
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetRolesQueryHandler_NoRolesFail()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<BlogAuthDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new BlogAuthDbContext(options);
        var userManager = RoleManagerMock.Create(context);
        var handler = new GetRolesQueryHandler(userManager.Object, _mapper);

        //Act
        var result = await handler.Handle(
            new GetRolesQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetRolesDto>>();
    }
}