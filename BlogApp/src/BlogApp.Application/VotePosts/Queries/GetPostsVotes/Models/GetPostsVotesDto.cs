using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.VotePosts.Queries.GetPostsVotes.Models;

public class GetPostsVotesDto : IMapFrom<VotePost>
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public bool IsUpvoted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VotePost, GetPostsVotesDto>();
    }
}
