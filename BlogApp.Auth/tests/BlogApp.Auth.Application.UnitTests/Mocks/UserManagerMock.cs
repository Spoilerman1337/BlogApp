using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;

namespace BlogApp.Auth.Application.UnitTests.Mocks;

public static class UserManagerMock
{
    public static Mock<UserManager<AppUser>> Create(BlogAuthDbContext context)
    {
        var userStore = new Mock<IUserStore<AppUser>>().Object;
        var userManager = new Mock<UserManager<AppUser>>(userStore, null, null, null, null, null, null, null, null);

        userManager.Object.UserValidators.Add(new UserValidator<AppUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<AppUser>());

        userManager.Setup(x => x.Users).Returns(context.Users);
        userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((string userId) => context.Users.Find(Guid.Parse(userId)));
        userManager.Setup(x => x.DeleteAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success).Callback<AppUser>((x) => context.Users.Remove(x).Context.SaveChanges());
        userManager.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<AppUser, string>((x, y) => context.Users.Add(x).Context.SaveChanges());
        userManager.Setup(x => x.UpdateAsync(It.IsAny<AppUser>())).ReturnsAsync(IdentityResult.Success).Callback<AppUser>((x) => context.SaveChanges());

        return userManager;
    }
}
