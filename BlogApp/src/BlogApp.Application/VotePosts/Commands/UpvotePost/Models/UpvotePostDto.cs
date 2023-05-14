using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.VotePosts.Commands.UpvotePost.Models;

public class UpvotePostDto : IMapFrom<UpvotePostCommand>
{
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public bool IsUpvoted { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UpvotePostDto, UpvotePostCommand>();
    }
}
