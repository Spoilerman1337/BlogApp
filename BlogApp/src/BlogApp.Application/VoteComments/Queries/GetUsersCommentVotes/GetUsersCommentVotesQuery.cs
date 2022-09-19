using BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes.Models;
using MediatR;

namespace BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes;

public class GetUsersCommentVotesQuery : IRequest<List<GetUsersCommentVotesDto>>
{
    public Guid UserId { get; set; }
}
