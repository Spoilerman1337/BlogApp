using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Comments.Commands.UpdateComment.Models;

public class UpdateCommentDto : IMapFrom<UpdateCommentCommand>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    public Guid ParentCommentId { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateCommentDto, UpdateCommentCommand>();
    }
}
