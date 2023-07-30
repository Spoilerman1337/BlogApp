using BlogApp.Infrastructure.Persistance;
using Mapster;
using Mapster.Utils;
using MapsterMapper;
using System.Reflection;
using Xunit;

namespace BlogApp.Application.UnitTests.Common;

public sealed class QueryTestClassFixture : IDisposable
{
    internal readonly BlogDbContext _context;
    internal readonly IMapper _mapper;

    public QueryTestClassFixture()
    {
        _context = BlogAppContextFactory.Create();
        _mapper = new Mapper();
    }

    public void Dispose()
    {
        BlogAppContextFactory.Destroy(_context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestClassFixture> { }
