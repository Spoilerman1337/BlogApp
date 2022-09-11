using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BlogApp.Auth.Application.UnitTests.Mocks;

public static class UserManagerMock
{
    public static Mock<UserManager<TUser>> Create<TUser>(BlogAuthDbContext context, DbSet<TUser> userList) where TUser : class
    {
        var userStore = new Mock<IUserStore<TUser>>().Object;
        var userManager = new Mock<UserManager<TUser>>(userStore, null, null, null, null, null, null, null, null);

        userManager.Object.UserValidators.Add(new UserValidator<TUser>());
        userManager.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

        userManager.Setup(x => x.Users).Returns(userList);
        userManager.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success).Callback<TUser>((x) => userList.Remove(x).Context.SaveChanges());
        userManager.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => userList.Add(x).Context.SaveChanges());
        userManager.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success).Callback<TUser>((x) => context.SaveChanges());

        return userManager;
    }
}
