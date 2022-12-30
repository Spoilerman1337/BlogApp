using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetTags.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Tags.Queries.GetTags;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<GetTagsDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetTagsQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetTagsDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Tags.Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                    .Take(request.PageAmount ?? int.MaxValue)
                                    .OrderBy(c => c.Id)
                                    .ProjectTo<GetTagsDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
    }
}
