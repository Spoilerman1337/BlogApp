using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Comments.Queries.GetComment.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Comments.Queries.GetComment;

public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, GetCommentDto>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCommentQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<GetCommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Comments.Where(c => c.UserId == request.UserId)
            .ProjectTo<GetCommentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == request.Id);
    }
}
