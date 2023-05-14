using BlogApp.Domain.Entites;
using Mapster;

namespace BlogApp.Application.Tags.Queries.GetPostsTags.Models;

public class GetPostsTagsDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string TagName { get; set; } = null!;
    public List<Guid> PostIds { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Tag, GetPostsTagsDto>()
               .Map(dest => dest.PostIds, src => src.Posts.Select(c => c.Id));
    }
}
