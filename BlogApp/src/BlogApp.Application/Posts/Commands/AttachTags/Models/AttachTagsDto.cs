using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.AttachTags.Models;

public class AttachTagsDto : IMapFrom<AttachTagsCommand>
{
    [Required]
    public List<Guid> TagIds { get; set; } = null!;
    [Required]
    public Guid Id { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<AttachTagsDto, AttachTagsCommand>();
    }
}
