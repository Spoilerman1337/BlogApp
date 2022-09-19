using BlogApp.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Infrastructure.Persistance.Configuration;

public class VoteCommentConfiguration : IEntityTypeConfiguration<VoteComment>
{
    public void Configure(EntityTypeBuilder<VoteComment> builder)
    {
        builder.HasKey(x => new { x.UserId, x.CommentId });
    }
}
