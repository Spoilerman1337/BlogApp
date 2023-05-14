using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Comments.Commands.CreateComment.Models;

public class CreateCommentDto : IMapFrom<CreateCommentCommand>
{
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    public Guid ParentCommentId { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommentDto, CreateCommentCommand>();
    }
}
