using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Tags.Commands.UpdateTag.Models;

public class UpdateTagDto : IMapFrom<UpdateTagCommand>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string TagName { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateTagDto, UpdateTagCommand>();
    }
}
