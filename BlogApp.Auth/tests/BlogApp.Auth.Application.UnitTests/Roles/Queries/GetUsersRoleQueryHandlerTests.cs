using AutoMapper;
using BlogApp.Auth.Application.Roles.Queries.GetRoles.Models;
using BlogApp.Auth.Application.Roles.Queries.GetRoles;
using BlogApp.Auth.Application.UnitTests.Common;
using BlogApp.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using FluentAssertions;
using BlogApp.Auth.Application.Roles.Queries.GetUsersRole;
using System;
using BlogApp.Auth.Application.Roles.Queries.GetUsersRole.Models;
using BlogApp.Auth.Application.UnitTests.Mocks;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Application.Users.Queries.GetUsersByRole;

namespace BlogApp.Auth.Application.UnitTests.Roles.Queries;

[Collection("QueryCollection")]
public class GetUsersRoleQueryHandlerTests
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
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
