using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetUsersPosts.Models;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts;

public class GetUsersPostsQueryHandler : IRequestHandler<GetUsersPostsQuery, List<GetUsersPostsDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetUsersPostsQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetUsersPostsDto>> Handle(GetUsersPostsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts.Where(p => p.UserId == request.UserId &&
                                                 (!request.From.HasValue || p.CreationTime >= request.From) &&
                                                 (!request.To.HasValue || p.CreationTime <= request.To))
                                     .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                     .Take(request.PageAmount ?? int.MaxValue)
                                     .ProjectToType<GetUsersPostsDto>()
                                     .ToListAsync(cancellationToken);
    }
}
