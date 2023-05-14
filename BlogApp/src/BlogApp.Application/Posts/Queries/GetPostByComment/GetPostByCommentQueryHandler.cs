using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostByComment.Models;
using BlogApp.Domain.Entites;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Queries.GetPostByComment;

public class GetPostByCommentQueryHandler : IRequestHandler<GetPostByCommentQuery, GetPostByCommentDto>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPostByCommentQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<GetPostByCommentDto> Handle(GetPostByCommentQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.Comments.Select(c => c.Id).Contains(request.CommentId) && !_dbContext.Posts.Where(c => c.Comments.Select(x => x.Id).Contains(request.CommentId)).Any())
            throw new NotFoundException(nameof(Comment), request.CommentId);

        var post = await _dbContext.Posts.Include(p => p.Comments)
                                         .FirstAsync(p => p.Comments
                                            .Select(p => p.Id)
                                            .Contains(request.CommentId), cancellationToken);

        return _mapper.Map<GetPostByCommentDto>(post);
    }
}
