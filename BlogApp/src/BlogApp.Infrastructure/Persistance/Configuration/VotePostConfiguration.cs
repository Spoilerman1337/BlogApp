using BlogApp.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Infrastructure.Persistance.Configuration;

public class VotePostConfiguration : IEntityTypeConfiguration<VotePost>
{
    public void Configure(EntityTypeBuilder<VotePost> builder)
    {
        builder.HasKey(x => new { x.UserId, x.PostId });
    }
}
