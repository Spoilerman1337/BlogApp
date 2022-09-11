using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BlogApp.Auth.Application.UnitTests.Common;

public class BlogAppContextFactory
{
    protected internal static Guid ToBeUpdatedUserId = Guid.NewGuid();
    protected internal static Guid ToBeDeletedUserId = Guid.NewGuid();
    protected internal static Guid ToBeUpdatedRoleId = Guid.NewGuid();
    protected internal static Guid ToBeDeletedRoleId = Guid.NewGuid();

    public static BlogAuthDbContext Create()
    {
        var options = new DbContextOptionsBuilder<BlogAuthDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        var context = new BlogAuthDbContext(options);

        var users = new List<AppUser>
        {
            new AppUser
            {
                Id = Guid.Parse("FDAE39CF-B55E-47BC-9904-00DF3CFD6FAD"),
                UserName = "Aenean",
                Email = "Ullamcorper@lorem.com"
            },
            new AppUser
            {
                Id = ToBeUpdatedUserId,
                FirstName = "Condimentum",
                LastName = "Id",
                Patronymic = "Venenatis",
                UserName = "Sit",
                Email = "amet@lorem.com"
            },
            new AppUser
            {
                Id = ToBeDeletedUserId,
                UserName = "Imperdiet",
                Email = "lobortis@lorem.com"
            }
        };

        var roles = new List<AppRole>
        {
            new AppRole
            {
                Id = ToBeUpdatedRoleId,
                Name = "Donec",
                NormalizedName = "DONEC"
            },
            new AppRole
            {
                Id = ToBeDeletedRoleId,
                Name = "In",
                NormalizedName = "IN"
            }
        };

        var userRoles = new List<IdentityUserRole<Guid>>()
        {
            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("FDAE39CF-B55E-47BC-9904-00DF3CFD6FAD"),
                RoleId = ToBeUpdatedRoleId
            },
            new IdentityUserRole<Guid>
            {
                UserId = ToBeUpdatedUserId,
                RoleId = ToBeUpdatedRoleId
            },
            new IdentityUserRole<Guid>
            {
                UserId = ToBeDeletedUserId,
                RoleId = ToBeDeletedRoleId
            },
        };

        context.Users.AddRange(users);
        context.Roles.AddRange(roles);
        context.UserRoles.AddRange(userRoles);

        context.SaveChanges();

        return context;
    }

    public static void Destroy(BlogAuthDbContext context)
    {
        context.Dispose();
    }
}
