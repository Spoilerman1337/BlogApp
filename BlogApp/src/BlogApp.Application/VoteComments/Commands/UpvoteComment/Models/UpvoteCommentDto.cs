using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.VoteComments.Commands.UpvoteComment.Models;

public class UpvoteCommentDto : IMapFrom<UpvoteCommentCommand>
{
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public bool IsUpvoted { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UpvoteCommentDto, UpvoteCommentCommand>();
    }
}
