using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Comments.Queries.GetCommentsFromPost;

public class GetCommentsFromPostQueryHandler : IRequestHandler<GetCommentsFromPostQuery, List<GetCommentsFromPostDto>>
{
    private readonly IBlogDbContext _dbContext;

    public GetCommentsFromPostQueryHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<GetCommentsFromPostDto>> Handle(GetCommentsFromPostQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.Posts.Select(c => c.Id).Contains(request.PostId))
            throw new NotFoundException(nameof(Post), request.PostId);

        return await _dbContext.Comments.Where(c => c.Post.Id == request.PostId &&
                                                    (!request.From.HasValue || c.CreationTime >= request.From) &&
                                                    (!request.To.HasValue || c.CreationTime <= request.To))
                                        .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                        .Take(request.PageAmount ?? int.MaxValue)
                                        .OrderBy(c => c.Id)
                                        .ProjectToType<GetCommentsFromPostDto>()
                                        .ToListAsync(cancellationToken);
    }
}
