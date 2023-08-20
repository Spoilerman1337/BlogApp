using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogApp.Auth.Application.UnitTests.Mocks;

public static class UserManagerMock
{
    public static Mock<UserManager<UserEntity>> Create(BlogAuthDbContext context)
    {
        var userStore = new Mock<IUserStore<UserEntity>>().Object;
        var userManager = new Mock<UserManager<UserEntity>>(userStore, null, null, null, null, null, null, null, null);

        userManager.Object.UserValidators.Add(new UserValidator<UserEntity>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<UserEntity>());

        userManager.Setup(x => x.Users)
            .Returns(context.Users);

        userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync((string userId) => context.Users.Find(Guid.Parse(userId)));
        userManager.Setup(x => x.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((string userName) => context.Users.FirstOrDefault(x => x.UserName == userName));
        userManager.Setup(x => x.GetRolesAsync(It.IsAny<UserEntity>()))
            .ReturnsAsync((UserEntity user) => context.Roles.Where(c => context.UserRoles.Where(c => c.UserId == user.Id).Select(c => c.RoleId).Contains(c.Id)).Select(c => c.Name!).ToList());
        userManager.Setup(x => x.RemoveFromRolesAsync(It.IsAny<UserEntity>(), It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<UserEntity, IEnumerable<string>>((x, y) => context.UserRoles.Remove(context.UserRoles.Where(a => a.UserId == x.Id && a.RoleId.Equals(context.Roles.Where(c => c.Name == y.First()).First().Id)).First()).Context.SaveChanges());
        userManager.Setup(x => x.AddToRoleAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<UserEntity, string>((x, y) => context.UserRoles.Add(new IdentityUserRole<Guid> { RoleId = context.Roles.Where(c => c.Name == y).First().Id, UserId = x.Id }).Context.SaveChanges());
        userManager.Setup(x => x.DeleteAsync(It.IsAny<UserEntity>()))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<UserEntity>((x) => context.Users.Remove(x).Context.SaveChanges());
        userManager.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<UserEntity, string>((x, y) => context.Users.Add(x).Context.SaveChanges());
        userManager.Setup(x => x.UpdateAsync(It.IsAny<UserEntity>()))
            .ReturnsAsync(IdentityResult.Success)
            .Callback<UserEntity>((x) => context.SaveChanges());
        userManager.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>()))
            .ReturnsAsync((string roleName) => context.Users.Where(c => context.UserRoles.Where(c => c.RoleId == context.Roles.First(c => c.Name == roleName).Id).Select(c => c.UserId).ToList().Contains(c.Id)).ToList());

        return userManager;
    }
}
