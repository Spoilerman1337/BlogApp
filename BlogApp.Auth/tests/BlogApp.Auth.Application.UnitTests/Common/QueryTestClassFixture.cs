using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Application.Common.Mappings;
using BlogApp.Auth.Infrastructure.Persistance;
using System;
using System.Reflection;

namespace BlogApp.Auth.Application.UnitTests.Common;

public class QueryTestClassFixture : IDisposable
{
    internal readonly BlogAuthDbContext _context;
    internal readonly IMapper _mapper;

    public QueryTestClassFixture()
    {
        _context = BlogAppContextFactory.Create();

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