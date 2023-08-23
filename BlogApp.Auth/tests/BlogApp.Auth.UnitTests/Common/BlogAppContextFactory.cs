using System;
using System.Collections.Generic;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.UnitTests.Common;

public class BlogAppContextFactory
{
    protected internal static readonly Guid ToBeUpdatedUserId = Guid.NewGuid();
    protected internal static readonly Guid ToBeDeletedUserId = Guid.NewGuid();
    protected internal static readonly Guid ToBeUpdatedRoleId = Guid.NewGuid();
    protected internal static readonly Guid ToBeDeletedRoleId = Guid.NewGuid();

    public static BlogAuthDbContext Create()
    {
        var options = new DbContextOptionsBuilder<BlogAuthDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new BlogAuthDbContext(options);

        var users = new List<UserEntity>
        {
            new()
            {
                Id = Guid.Parse("FDAE39CF-B55E-47BC-9904-00DF3CFD6FAD"),
                UserName = "Aenean",
                Email = "Ullamcorper@lorem.com"
            },
            new()
            {
                Id = ToBeUpdatedUserId,
                FirstName = "Condimentum",
                LastName = "Id",
                Patronymic = "Venenatis",
                UserName = "Sit",
                Email = "amet@lorem.com"
            },
            new()
            {
                Id = ToBeDeletedUserId,
                UserName = "Imperdiet",
                Email = "lobortis@lorem.com"
            }
        };

        var roles = new List<RoleEntity>
        {
            new()
            {
                Id = Guid.Parse("5C29FCAB-1AAD-4EDD-8D75-3770D98D651D"),
                Name = "Aliquet",
                NormalizedName = "ALIQUET"
            },
            new()
            {
                Id = ToBeUpdatedRoleId,
                Name = "Donec",
                NormalizedName = "DONEC"
            },
            new()
            {
                Id = ToBeDeletedRoleId,
                Name = "In",
                NormalizedName = "IN"
            }
        };

        var userRoles = new List<IdentityUserRole<Guid>>
        {
            new()
            {
                UserId = Guid.Parse("FDAE39CF-B55E-47BC-9904-00DF3CFD6FAD"),
                RoleId = ToBeUpdatedRoleId
            },
            new()
            {
                UserId = ToBeUpdatedUserId,
                RoleId = ToBeUpdatedRoleId
            },
            new()
            {
                UserId = ToBeDeletedUserId,
                RoleId = ToBeDeletedRoleId
            }
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