using BlogApp.Domain.Entites;
using Mapster;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts.Models;

public class GetUsersPostsDto : IMapFrom<Post>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Header { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
    public List<Guid> CommentIds { get; set; } = null!;
    public List<Guid> TagIds { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Post, GetUsersPostsDto>()
               .Map(dest => dest.TagIds, src => src.Tags.Select(c => c.Id))
               .Map(dest => dest.CommentIds, src => src.Comments.Select(c => c.Id));
    }
}
