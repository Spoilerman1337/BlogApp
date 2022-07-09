using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostByComment.Models;
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
        return await _dbContext.Posts.ProjectTo<GetPostByCommentDto>(_mapper.ConfigurationProvider)
                                     .Where(c => c.CommentIds.Contains(request.CommentId))
                                     .SingleOrDefaultAsync(cancellationToken);
    }
}
