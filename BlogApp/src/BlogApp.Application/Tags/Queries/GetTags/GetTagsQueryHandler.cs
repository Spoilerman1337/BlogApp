using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetTags.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Tags.Queries.GetTags;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<GetTagsDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetTagsQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetTagsDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Tags.Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                    .Take(request.PageAmount ?? int.MaxValue)
                                    .OrderBy(c => c.Id)
                                    .ProjectToType<GetTagsDto>()
                                    .ToListAsync(cancellationToken);
    }
}
