using System;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using BlogApp.Auth.UnitTests.Mocks;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace BlogApp.Auth.UnitTests.Common;

public sealed class QueryTestClassFixture : IDisposable
{
    internal readonly BlogAuthDbContext _context;
    internal readonly IMapper _mapper;
    internal readonly RoleManager<RoleEntity> _roleManager;
    internal readonly UserManager<UserEntity> _userManager;

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
public class QueryCollection : ICollectionFixture<QueryTestClassFixture>
{
}