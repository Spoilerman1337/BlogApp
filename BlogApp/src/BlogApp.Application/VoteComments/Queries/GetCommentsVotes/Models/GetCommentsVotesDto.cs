using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.VoteComments.Queries.GetCommentsVotes.Models;

public class GetCommentsVotesDto : IMapFrom<VoteComment>
{
    public Guid CommentId { get; set; }
    public Guid UserId { get; set; }
    public bool IsUpvoted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VoteComment, GetCommentsVotesDto>();
    }
}
