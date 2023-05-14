using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.UpdatePost.Models;

public class UpdatePostDto : IMapFrom<UpdatePostCommand>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Header { get; set; } = null!;
    public string Text { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UpdatePostDto, UpdatePostCommand>();
    }
}
