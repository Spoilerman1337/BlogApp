using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetUsersPosts.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts;

public class GetUsersPostsQueryHandler : IRequestHandler<GetUsersPostsQuery, List<GetUsersPostsDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUsersPostsQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetUsersPostsDto>> Handle(GetUsersPostsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts.Where(p => p.UserId == request.UserId)
                                     .ProjectTo<GetUsersPostsDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
    }
}
