using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Comments.Queries.GetComments.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogApp.Application.Comments.Queries.GetComments;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<GetCommentsDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCommentsQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetCommentsDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.Where(c => (!request.From.HasValue || c.CreationTime >= request.From) && (!request.To.HasValue || c.CreationTime <= request.To))
                                        .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                        .Take(request.PageAmount ?? int.MaxValue)
                                        .OrderBy(c => c.Id)
                                        .ProjectTo<GetCommentsDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync(cancellationToken);
    }
}
