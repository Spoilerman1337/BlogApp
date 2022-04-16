using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Comments.Queries.GetCommentsFromPost;

public class GetCommentFromPostQueryHandler : IRequestHandler<GetCommentFromPostQuery, List<GetCommentFromPostDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCommentFromPostQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetCommentFromPostDto>> Handle(GetCommentFromPostQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.Where(c => c.Post.Id == request.PostId)
                                        .OrderBy(c => c.Id)
                                        .ProjectTo<GetCommentFromPostDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
    }
}
