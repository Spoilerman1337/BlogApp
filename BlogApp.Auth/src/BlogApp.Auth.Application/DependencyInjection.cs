using BlogApp.Auth.Application.Common.Identity;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Application.Common.Mappings;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogApp.Auth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new MappingProfile(Assembly.GetExecutingAssembly()));
            config.AddProfile(new MappingProfile(typeof(IBlogAuthDbContext).Assembly));
        });
        services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
                .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
                .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                .AddInMemoryClients(IdentityConfiguration.Clients)
                .AddDeveloperSigningCredential();
        return services;
    }
}
