using BlogApp.Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.IntegrationTests.Common;

public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<BlogDbContext>));

            if (dbContextDescriptor != null) 
                services.Remove(dbContextDescriptor);

            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseInMemoryDatabase("BlogAppDb");
            });
        });
    }
}