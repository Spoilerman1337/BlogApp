using BlogApp.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Common.Interfaces;

public interface IBlogDbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<VotePost> VotePosts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
