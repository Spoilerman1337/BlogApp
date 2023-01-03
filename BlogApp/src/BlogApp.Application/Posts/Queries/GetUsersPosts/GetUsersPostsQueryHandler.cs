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
        return await _dbContext.Posts.Where(p => p.UserId == request.UserId &&
                                                 (!request.From.HasValue || p.CreationTime >= request.From) &&
                                                 (!request.To.HasValue || p.CreationTime <= request.To))
                                     .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                     .Take(request.PageAmount ?? int.MaxValue)
                                     .ProjectTo<GetUsersPostsDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
    }
}
