using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.VotePosts.Queries.GetPostsVotes.Models;
using BlogApp.Domain.Entites;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VotePosts.Queries.GetPostsVotes;

public class GetPostsVotesQueryHandler : IRequestHandler<GetPostsVotesQuery, List<GetPostsVotesDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetPostsVotesQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetPostsVotesDto>> Handle(GetPostsVotesQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.Posts.Select(c => c.Id).Contains(request.PostId))
            throw new NotFoundException(nameof(Post), request.PostId);

        return await _dbContext.VotePosts.Where(c => c.PostId == request.PostId)
                                     .ProjectToType<GetPostsVotesDto>()
                                     .ToListAsync(cancellationToken);
    }
}
