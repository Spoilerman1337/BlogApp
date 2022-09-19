using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.VoteComments.Queries.GetCommentsVotes.Models;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VoteComments.Queries.GetCommentsVotes;

public class GetCommentsVotesQueryHandler : IRequestHandler<GetCommentsVotesQuery, List<GetCommentsVotesDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCommentsVotesQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetCommentsVotesDto>> Handle(GetCommentsVotesQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.Comments.Select(c => c.Id).Contains(request.CommentId))
            throw new NotFoundException(nameof(Comment), request.CommentId);

        return await _dbContext.VoteComments.Where(c => c.CommentId == request.CommentId)
                                     .ProjectTo<GetCommentsVotesDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
    }
}
