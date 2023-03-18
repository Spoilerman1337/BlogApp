using BlogApp.Auth.Application.UnitTests.Mocks;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using System;

namespace BlogApp.Auth.Application.UnitTests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly UserManager<AppUser> UserManager;
    protected readonly RoleManager<AppRole> RoleManager;
    protected readonly BlogAuthDbContext Context;

    public TestCommandBase()
    {
        Context = BlogAppContextFactory.Create();
        UserManager = UserManagerMock.Create(Context).Object;
        RoleManager = RoleManagerMock.Create(Context).Object;
    }

    public void Dispose()
    {
        BlogAppContextFactory.Destroy(Context);
        GC.SuppressFinalize(this);
    }
}