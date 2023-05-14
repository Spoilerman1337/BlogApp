using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostsByTag.Models;
using BlogApp.Domain.Entites;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Queries.GetPostsByTag;

public class GetPostsByTagQueryHandler : IRequestHandler<GetPostsByTagQuery, List<GetPostsByTagDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetPostsByTagQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetPostsByTagDto>> Handle(GetPostsByTagQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.Tags.Select(c => c.Id).Contains(request.TagId))
            throw new NotFoundException(nameof(Tag), request.TagId);

        return await _dbContext.Posts.Where(p => p.Tags.Select(c => c.Id).Contains(request.TagId) &&
                                                 (!request.From.HasValue || p.CreationTime >= request.From) &&
                                                 (!request.To.HasValue || p.CreationTime <= request.To))
                                     .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                     .Take(request.PageAmount ?? int.MaxValue)
                                     .OrderBy(c => c.Id)
                                     .ProjectToType<GetPostsByTagDto>()
                                     .ToListAsync(cancellationToken);
    }
}
