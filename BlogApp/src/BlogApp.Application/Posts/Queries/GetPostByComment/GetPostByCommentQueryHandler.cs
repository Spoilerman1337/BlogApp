using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostByComment.Models;
using BlogApp.Domain.Entites;
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
        if (!_dbContext.Comments.Select(c => c.Id).Contains(request.CommentId))
            throw new NotFoundException(nameof(Comment), request.CommentId);

        var post = await _dbContext.Posts.Include(p => p.Comments)
                                         .FirstOrDefaultAsync(p => p.Comments
                                            .Select(p => p.Id)
                                            .Contains(request.CommentId), cancellationToken);

        return _mapper.Map<GetPostByCommentDto>(post);
    }
}
