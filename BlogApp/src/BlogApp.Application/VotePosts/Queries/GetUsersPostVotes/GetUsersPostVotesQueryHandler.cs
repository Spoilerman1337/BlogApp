using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VotePosts.Queries.GetUsersPostVotes
{
    public class GetUsersPostVotesQueryHandler : IRequestHandler<GetUsersPostVotesQuery, List<GetUsersPostVotesDto>>
    {
        private readonly IBlogDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersPostVotesQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<List<GetUsersPostVotesDto>> Handle(GetUsersPostVotesQuery request, CancellationToken cancellationToken)
        {
            var userPosts = _dbContext.Posts.Where(c => c.UserId == request.UserId);

            return await _dbContext.VotePosts.Where(c => userPosts.Contains(c.Post))
                                     .ProjectTo<GetUsersPostVotesDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
        }
    }
}
