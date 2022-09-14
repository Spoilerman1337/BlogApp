using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;

namespace BlogApp.Auth.Application.UnitTests.Mocks;

public class RoleManagerMock
{
    public static Mock<RoleManager<AppRole>> Create(BlogAuthDbContext context)
    {
        var roleStore = new Mock<IRoleStore<AppRole>>().Object;
        var roleManager = new Mock<RoleManager<AppRole>>(roleStore, null, null, null, null);

        roleManager.Object.RoleValidators.Add(new RoleValidator<AppRole>());

        roleManager.Setup(x => x.Roles).Returns(context.Roles);
        roleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((string roleId) => context.Roles.Find(Guid.Parse(roleId)));
        roleManager.Setup(x => x.DeleteAsync(It.IsAny<AppRole>())).ReturnsAsync(IdentityResult.Success).Callback<AppRole>((x) => context.Roles.Remove(x).Context.SaveChanges()); ;
        roleManager.Setup(x => x.CreateAsync(It.IsAny<AppRole>())).ReturnsAsync(IdentityResult.Success).Callback<AppRole>((x) => context.Roles.Add(x).Context.SaveChanges());
        roleManager.Setup(x => x.UpdateAsync(It.IsAny<AppRole>())).ReturnsAsync(IdentityResult.Success).Callback<AppRole>((x) => context.SaveChanges()); ;

        return roleManager;
    }
}
