using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetPostsTags.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Tags.Queries.GetPostsTags;

public class GetPostsTagsQueryHandler : IRequestHandler<GetPostsTagsQuery, List<GetPostsTagsDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPostsTagsQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetPostsTagsDto>> Handle(GetPostsTagsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Tags.SelectMany(t => t.Posts.Where(p => p.Id == request.PostId))
                                    .OrderBy(c => c.Id)
                                    .ProjectTo<GetPostsTagsDto>(_mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);
    }
}
