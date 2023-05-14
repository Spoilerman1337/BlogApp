﻿using BlogApp.Domain.Entites;
using Mapster;

namespace BlogApp.Application.VotePosts.Queries.GetUsersPostVotes.Models;

public class GetUsersPostVotesDto : IMapFrom<VotePost>
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public bool IsUpvoted { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<VotePost, GetUsersPostVotesDto>();
    }
}
