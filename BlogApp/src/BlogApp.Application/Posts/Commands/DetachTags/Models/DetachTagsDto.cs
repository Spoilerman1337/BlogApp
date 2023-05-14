using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.DetachTags.Models;

public class DetachTagsDto : IMapFrom<DetachTagsCommand>
{
    [Required]
    public List<Guid> TagIds { get; set; } = null!;
    [Required]
    public Guid Id { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<DetachTagsDto, DetachTagsCommand>();
    }
}
