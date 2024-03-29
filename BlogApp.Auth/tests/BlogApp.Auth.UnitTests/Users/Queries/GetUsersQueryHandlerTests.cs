﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlogApp.Auth.Application.Users.Queries.GetUsers;
using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using BlogApp.Auth.UnitTests.Common;
using BlogApp.Auth.UnitTests.Mocks;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BlogApp.Auth.UnitTests.Users.Queries;

[Collection("QueryCollection")]
public class GetUsersQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

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
    public async Task GetUsersQueryHandlerPagination_Success()
    {
        //Arrange
        var handler = new GetUsersQueryHandler(_userManager, _mapper);

        //Act
        var result = await handler.Handle(
            new GetUsersQuery
            {
                Page = 0,
                PageAmount = 2
            },
            CancellationToken.None
        );

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<GetUsersDto>>();
        result.Should().HaveCount(2);
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