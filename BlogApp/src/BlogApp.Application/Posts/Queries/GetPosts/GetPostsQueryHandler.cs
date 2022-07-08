using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPosts.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Queries.GetPosts;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<GetPostsDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPostsQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetPostsDto>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts.OrderBy(c => c.Id)
                                     .ProjectTo<GetPostsDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
    }
}
