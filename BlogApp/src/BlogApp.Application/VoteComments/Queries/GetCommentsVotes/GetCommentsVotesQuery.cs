using BlogApp.Application.VoteComments.Queries.GetCommentsVotes.Models;
using MediatR;

namespace BlogApp.Application.VoteComments.Queries.GetCommentsVotes;

public class GetCommentsVotesQuery : IRequest<List<GetCommentsVotesDto>>
{
    public Guid CommentId { get; set; }
}
