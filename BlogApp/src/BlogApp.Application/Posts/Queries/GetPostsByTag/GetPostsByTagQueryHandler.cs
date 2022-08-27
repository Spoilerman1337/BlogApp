using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostsByTag.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Queries.GetPostsByTag;

public class GetPostsByTagQueryHandler : IRequestHandler<GetPostsByTagQuery, List<GetPostsByTagDto>>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPostsByTagQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<List<GetPostsByTagDto>> Handle(GetPostsByTagQuery request, CancellationToken cancellationToken)
    {
        if (!_dbContext.Tags.Select(c => c.Id).Contains(request.TagId))
            throw new NotFoundException(nameof(GetPostsByTagQueryHandler), request.TagId);

        return await _dbContext.Posts.Where(c => c.Tags.Select(c => c.Id).Contains(request.TagId))
                                     .OrderBy(c => c.Id)
                                     .ProjectTo<GetPostsByTagDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
    }
}
