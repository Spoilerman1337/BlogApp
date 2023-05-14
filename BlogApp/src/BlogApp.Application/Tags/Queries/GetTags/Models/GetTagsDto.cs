using BlogApp.Domain.Entites;
using Mapster;

namespace BlogApp.Application.Tags.Queries.GetTags.Models;

public class GetTagsDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string TagName { get; set; } = null!;
    public List<Guid> PostIds { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Tag, GetTagsDto>().Map(dest => dest.PostIds, src => src.Posts.Select(c => c.Id));
    }
}
