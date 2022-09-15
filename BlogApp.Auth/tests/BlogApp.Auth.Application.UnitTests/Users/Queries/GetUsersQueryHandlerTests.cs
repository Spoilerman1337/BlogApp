using AutoMapper;
using BlogApp.Auth.Application.UnitTests.Common;
using BlogApp.Auth.Application.UnitTests.Mocks;
using BlogApp.Auth.Application.Users.Queries.GetUsers;
using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BlogApp.Auth.Application.UnitTests.Users.Queries;

[Collection("QueryCollection")]
public class GetUsersQueryHandlerTests
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public GetUsersQueryHandlerTests(QueryTestClassFixture fixture)
    {
        _userManager = fixture._userManager;
        _mapper = fixture._mapper;
    }

    [Fact]
    public async Task GetUsersQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUsersQueryHandler(_userManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersDto>>();
    }

    [Fact]
    public async Task GetUsersQueryHandler_NoUsersFail()
    {
        //Arrange
        var options = new DbContextOptionsBuilder<BlogAuthDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new BlogAuthDbContext(options);
        var userManager = UserManagerMock.Create(context);
        var handler = new GetUsersQueryHandler(userManager.Object, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersQuery(),
            CancellationToken.None
        );

        //Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<GetUsersDto>>();
    }
}
