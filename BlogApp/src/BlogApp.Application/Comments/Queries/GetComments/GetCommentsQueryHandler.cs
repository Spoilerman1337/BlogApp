using BlogApp.Application.Comments.Queries.GetComments.Models;
using BlogApp.Application.Common.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Comments.Queries.GetComments;

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<GetCommentsDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetCommentsQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetCommentsDto>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.Where(c => (!request.From.HasValue || c.CreationTime >= request.From) && (!request.To.HasValue || c.CreationTime <= request.To))
                                        .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                        .Take(request.PageAmount ?? int.MaxValue)
                                        .OrderBy(c => c.Id)
                                        .ProjectToType<GetCommentsDto>()
                                        .ToListAsync(cancellationToken);
    }
}
