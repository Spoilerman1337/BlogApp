using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Comments.Queries.GetUsersComments.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Comments.Queries.GetUsersComments;

public class GetUsersCommentsQueryHandler : IRequestHandler<GetUsersCommentsQuery, List<GetUsersCommentsDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUsersCommentsQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetUsersCommentsDto>> Handle(GetUsersCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.Where(c => c.UserId == request.UserId)
                                        .OrderBy(c => c.Id)
                                        .ProjectTo<GetUsersCommentsDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
    }
}
