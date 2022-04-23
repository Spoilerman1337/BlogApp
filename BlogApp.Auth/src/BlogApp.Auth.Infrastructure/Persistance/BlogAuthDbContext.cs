using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Infrastructure.Persistance;

public class BlogAuthDbContext : IdentityDbContext<AppUser>, IBlogAuthDbContext
{
    public BlogAuthDbContext(DbContextOptions<BlogAuthDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));

        builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
        builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));
        builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaims"));
        builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));
        builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));
        builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));

        builder.ApplyConfiguration(new AppUserConfiguration());
    }
}
