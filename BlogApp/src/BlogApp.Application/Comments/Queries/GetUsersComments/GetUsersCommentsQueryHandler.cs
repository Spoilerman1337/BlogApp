using BlogApp.Application.Comments.Queries.GetUsersComments.Models;
using BlogApp.Application.Common.Interfaces;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Comments.Queries.GetUsersComments;

public class GetUsersCommentsQueryHandler : IRequestHandler<GetUsersCommentsQuery, List<GetUsersCommentsDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetUsersCommentsQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetUsersCommentsDto>> Handle(GetUsersCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.Where(c => c.UserId == request.UserId &&
                                                    (!request.From.HasValue || c.CreationTime >= request.From) &&
                                                    (!request.To.HasValue || c.CreationTime <= request.To))
                                        .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                        .Take(request.PageAmount ?? int.MaxValue)
                                        .OrderBy(c => c.Id)
                                        .ProjectToType<GetUsersCommentsDto>()
                                        .ToListAsync(cancellationToken);
    }
}
