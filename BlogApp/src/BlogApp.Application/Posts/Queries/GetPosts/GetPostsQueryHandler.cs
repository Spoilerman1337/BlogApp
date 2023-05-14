using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPosts.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Queries.GetPosts;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<GetPostsDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetPostsQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetPostsDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts.Where(p => (!request.From.HasValue || p.CreationTime >= request.From) &&
                                                 (!request.To.HasValue || p.CreationTime <= request.To))
                                     .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                     .Take(request.PageAmount ?? int.MaxValue)
                                     .OrderBy(c => c.Id)
                                     .ProjectToType<GetPostsDto>()
                                     .ToListAsync(cancellationToken);
    }
}
