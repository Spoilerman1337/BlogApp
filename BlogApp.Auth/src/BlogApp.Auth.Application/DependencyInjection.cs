using BlogApp.Auth.Application.Common.Identity;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Auth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
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
