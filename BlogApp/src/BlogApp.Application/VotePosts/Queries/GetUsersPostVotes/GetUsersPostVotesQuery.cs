using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes.Models;
using MediatR;

namespace BlogApp.Application.VotePosts.Queries.GetUsersPostVotes;

public class GetUsersPostVotesQuery : IRequest<List<GetUsersPostVotesDto>>
{
    public Guid UserId { get; set; }
}
