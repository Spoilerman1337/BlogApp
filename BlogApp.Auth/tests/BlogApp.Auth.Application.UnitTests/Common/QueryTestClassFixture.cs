using BlogApp.Auth.Application.UnitTests.Mocks;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System;
using Xunit;

namespace BlogApp.Auth.Application.UnitTests.Common;

public sealed class QueryTestClassFixture : IDisposable
{
    internal readonly BlogAuthDbContext _context;
    internal readonly UserManager<UserEntity> _userManager;
    internal readonly RoleManager<RoleEntity> _roleManager;
    internal readonly IMapper _mapper;

    public QueryTestClassFixture()
    {
        _context = BlogAppContextFactory.Create();
        _userManager = UserManagerMock.Create(_context).Object;
        _roleManager = RoleManagerMock.Create(_context).Object;
        _mapper = new Mapper();
    }

    public void Dispose()
    {
        BlogAppContextFactory.Destroy(_context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestClassFixture> { }