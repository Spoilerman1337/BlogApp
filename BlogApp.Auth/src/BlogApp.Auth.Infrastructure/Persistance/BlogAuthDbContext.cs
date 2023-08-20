using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Infrastructure.Persistance;

public class BlogAuthDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>, IBlogAuthDbContext
{
    public BlogAuthDbContext(DbContextOptions<BlogAuthDbContext> options) : base(options) { }
            
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserEntity>(entity =>
        {
            entity.ToTable(name: "Users");
            entity.Property(p => p.Id).HasDefaultValueSql("newsequentialid()");
        });
        builder.Entity<RoleEntity>(entity =>
        {
            entity.ToTable(name: "Roles");
            entity.Property(p => p.Id).HasDefaultValueSql("newsequentialid()");
        });

        builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable(name: "UserRoles"));
        builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable(name: "UserClaims"));
        builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable(name: "UserLogins"));
        builder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable(name: "UserTokens"));
        builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable(name: "RoleClaims"));

        builder.ApplyConfiguration(new AppUserConfiguration());
        builder.ApplyConfiguration(new AppRoleConfiguration());
    }
}
