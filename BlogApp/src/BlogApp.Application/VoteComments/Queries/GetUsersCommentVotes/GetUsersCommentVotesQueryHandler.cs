using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VoteComments.Queries.GetUsersCommentVotes;

public class GetUsersCommentVotesQueryHandler : IRequestHandler<GetUsersCommentVotesQuery, List<GetUsersCommentVotesDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUsersCommentVotesQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetUsersCommentVotesDto>> Handle(GetUsersCommentVotesQuery request, CancellationToken cancellationToken)
    {
        var userComments = _dbContext.Comments.Where(c => c.UserId == request.UserId);

        return await _dbContext.VoteComments.Where(c => userComments.Contains(c.Comment))
                                 .ProjectTo<GetUsersCommentVotesDto>(_mapper.ConfigurationProvider)
                                 .ToListAsync(cancellationToken);
    }
}
