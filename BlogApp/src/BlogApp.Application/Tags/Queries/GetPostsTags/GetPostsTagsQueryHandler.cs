using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetPostsTags.Models;
using BlogApp.Domain.Entites;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Tags.Queries.GetPostsTags;

public class GetPostsTagsQueryHandler : IRequestHandler<GetPostsTagsQuery, List<GetPostsTagsDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetPostsTagsQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetPostsTagsDto>> Handle(GetPostsTagsQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.Posts.Select(c => c.Id).Contains(request.PostId))
            throw new NotFoundException(nameof(Post), request.PostId);

        return await _dbContext.Tags.Where(t => t.Posts.Where(p => p.Id == request.PostId).Any())
                                    .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                    .Take(request.PageAmount ?? int.MaxValue)
                                    .OrderBy(c => c.Id)
                                    .ProjectToType<GetPostsTagsDto>()
                                    .ToListAsync(cancellationToken);
    }
}
