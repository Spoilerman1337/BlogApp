using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Application.Common.Mappings;
using BlogApp.Auth.Application.UnitTests.Mocks;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using System;
using System.Reflection;
using Xunit;

namespace BlogApp.Auth.Application.UnitTests.Common;

public sealed class QueryTestClassFixture : IDisposable
{
    internal readonly BlogAuthDbContext _context;
    internal readonly UserManager<AppUser> _userManager;
    internal readonly RoleManager<AppRole> _roleManager;
    internal readonly IMapper _mapper;

    public QueryTestClassFixture()
    {
        _context = BlogAppContextFactory.Create();
        _userManager = UserManagerMock.Create(_context).Object;
        _roleManager = RoleManagerMock.Create(_context).Object;

        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new MappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new MappingProfile(typeof(IBlogAuthDbContext).Assembly));
        });
        _mapper = mapperConfiguration.CreateMapper();
    }

    public void Dispose()
    {
        BlogAppContextFactory.Destroy(_context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestClassFixture> { }