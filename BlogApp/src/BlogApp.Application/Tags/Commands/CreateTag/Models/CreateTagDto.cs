using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Tags.Commands.CreateTag.Models;

public class CreateTagDto : IMapFrom<CreateTagCommand>
{
    [Required]
    public string TagName { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTagDto, CreateTagCommand>();
    }
}
