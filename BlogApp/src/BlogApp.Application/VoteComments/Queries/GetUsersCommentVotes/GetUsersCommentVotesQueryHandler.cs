using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes;

public class GetUsersCommentVotesQueryHandler : IRequestHandler<GetUsersCommentVotesQuery, List<GetUsersCommentVotesDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetUsersCommentVotesQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetUsersCommentVotesDto>> Handle(GetUsersCommentVotesQuery request, CancellationToken cancellationToken)
    {
        var userComments = _dbContext.Comments.Where(c => c.UserId == request.UserId);

        return await _dbContext.VoteComments.Where(c => userComments.Contains(c.Comment))
                                 .ProjectToType<GetUsersCommentVotesDto>()
                                 .ToListAsync(cancellationToken);
    }
}
