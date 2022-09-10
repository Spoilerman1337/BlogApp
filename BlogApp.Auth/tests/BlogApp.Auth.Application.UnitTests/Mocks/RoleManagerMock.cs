using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BlogApp.Auth.Application.UnitTests.Mocks;

public class RoleManagerMock
{
    public static Mock<RoleManager<TRole>> Create<TRole>(BlogAuthDbContext context, DbSet<TRole> rolesList) where TRole : class
    {
        var roleStore = new Mock<IRoleStore<TRole>>().Object;
        var roleManager = new Mock<RoleManager<TRole>>(roleStore, null, null, null, null);

        roleManager.Object.RoleValidators.Add(new RoleValidator<TRole>());

        roleManager.Setup(x => x.DeleteAsync(It.IsAny<TRole>())).ReturnsAsync(IdentityResult.Success);
        roleManager.Setup(x => x.CreateAsync(It.IsAny<TRole>())).ReturnsAsync(IdentityResult.Success).Callback<TRole>((x) => rolesList.Add(x).Context.SaveChanges());
        roleManager.Setup(x => x.UpdateAsync(It.IsAny<TRole>())).ReturnsAsync(IdentityResult.Success);

        return roleManager;
    }
}
