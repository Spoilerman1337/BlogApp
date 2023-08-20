using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Linq;

namespace BlogApp.Auth.Application.UnitTests.Mocks;

public class RoleManagerMock
{
    public static Mock<RoleManager<RoleEntity>> Create(BlogAuthDbContext context)
    {
        var roleStore = new Mock<IRoleStore<RoleEntity>>().Object;
        var roleManager = new Mock<RoleManager<RoleEntity>>(roleStore, null, null, null, null);

        roleManager.Object.RoleValidators.Add(new RoleValidator<RoleEntity>());

        roleManager.Setup(x => x.Roles).Returns(context.Roles);
        roleManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string roleId) => context.Roles.Find(Guid.Parse(roleId)));
        roleManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((string roleName) => context.Roles.Where(c => c.Name == roleName).First());
        roleManager.Setup(x => x.DeleteAsync(It.IsAny<RoleEntity>()))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<RoleEntity>((x) => context.Roles.Remove(x).Context.SaveChanges()); ;
        roleManager.Setup(x => x.CreateAsync(It.IsAny<RoleEntity>()))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<RoleEntity>((x) => context.Roles.Add(x).Context.SaveChanges());
        roleManager.Setup(x => x.UpdateAsync(It.IsAny<RoleEntity>()))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<RoleEntity>((x) => context.SaveChanges()); ;

        return roleManager;
    }
}
