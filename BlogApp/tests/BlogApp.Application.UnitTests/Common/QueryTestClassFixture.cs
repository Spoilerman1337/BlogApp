using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Common.Mappings;
using BlogApp.Infrastructure.Persistance;
using System.Reflection;
using Xunit;

namespace BlogApp.Application.UnitTests.Common;

public class QueryTestClassFixture : IDisposable
{
    public readonly BlogDbContext _context;
    public readonly IMapper _mapper;

    public QueryTestClassFixture()
    {
        _context = BlogAppContextFactory.Create();

        var mapperConfiguration = new MapperConfiguration(config =>
        {
            config.AddProfile(new MappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new MappingProfile(typeof(IBlogDbContext).Assembly));
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
