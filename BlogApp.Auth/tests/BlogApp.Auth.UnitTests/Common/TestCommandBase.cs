using System;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using BlogApp.Auth.UnitTests.Mocks;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.UnitTests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly BlogAuthDbContext Context;
    protected readonly RoleManager<RoleEntity> RoleManager;
    protected readonly UserManager<UserEntity> UserManager;

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