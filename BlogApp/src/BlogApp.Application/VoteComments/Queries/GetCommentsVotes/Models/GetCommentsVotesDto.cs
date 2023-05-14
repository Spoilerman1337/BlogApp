using BlogApp.Domain.Entites;
using Mapster;

namespace BlogApp.Application.VoteComments.Queries.GetCommentsVotes.Models;

public class GetCommentsVotesDto : IMapFrom<VoteComment>
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
    public bool IsUpvoted { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<VoteComment, GetCommentsVotesDto>();
    }
}
