using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.VotePosts.Queries.GetPostsVotes.Models;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VotePosts.Queries.GetPostsVotes;

public class GetPostsVotesQueryHandler : IRequestHandler<GetPostsVotesQuery, List<GetPostsVotesDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPostsVotesQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetPostsVotesDto>> Handle(GetPostsVotesQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.Posts.Select(c => c.Id).Contains(request.PostId))
            throw new NotFoundException(nameof(Post), request.PostId);

        return await _dbContext.VotePosts.Where(c => c.PostId == request.PostId)
                                     .OrderBy(c => c.PostId)
                                     .ProjectTo<GetPostsVotesDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
    }
}
