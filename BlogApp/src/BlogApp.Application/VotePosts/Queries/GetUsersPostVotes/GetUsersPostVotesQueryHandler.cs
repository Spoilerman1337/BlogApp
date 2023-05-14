using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VotePosts.Queries.GetUsersPostVotes;

public class GetUsersPostVotesQueryHandler : IRequestHandler<GetUsersPostVotesQuery, List<GetUsersPostVotesDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetUsersPostVotesQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetUsersPostVotesDto>> Handle(GetUsersPostVotesQuery request, CancellationToken cancellationToken)
    {
        var userPosts = _dbContext.Posts.Where(c => c.UserId == request.UserId);

        return await _dbContext.VotePosts.Where(c => userPosts.Contains(c.Post))
                                 .ProjectToType<GetUsersPostVotesDto>()
                                 .ToListAsync(cancellationToken);
    }
}
