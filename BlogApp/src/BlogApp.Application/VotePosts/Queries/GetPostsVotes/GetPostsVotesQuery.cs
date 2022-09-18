using BlogApp.Application.VotePosts.Queries.GetPostsVotes.Models;
using MediatR;

namespace BlogApp.Application.VotePosts.Queries.GetPostsVotes;

public class GetPostsVotesQuery : IRequest<List<GetPostsVotesDto>>
{
    public Guid PostId { get; set; }
}
