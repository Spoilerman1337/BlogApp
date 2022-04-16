using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPost.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Queries.GetPost;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, GetPostDto>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPostQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<GetPostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts.ProjectTo<GetPostDto>(_mapper.ConfigurationProvider)
                                     .FirstOrDefaultAsync(c => c.Id == request.Id);
    }
}
