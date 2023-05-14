using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.CreatePost.Models;

public class CreatePostDto : IMapFrom<CreatePostCommand>
{
    [Required]
    public string Header { get; set; } = null!;
    public string Text { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<CreatePostDto, CreatePostCommand>();
    }
}
