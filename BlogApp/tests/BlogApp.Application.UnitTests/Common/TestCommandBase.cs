using BlogApp.Infrastructure.Persistance;

namespace BlogApp.Application.UnitTests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly BlogDbContext Context;

    public TestCommandBase()
    {
        Context = BlogAppContextFactory.Create();
    }

    public void Dispose()
    {
        BlogAppContextFactory.Destroy(Context);
    }

}
