namespace BlogApp.Auth.Application.Common.Interfaces;

public interface IBlogAuthDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
