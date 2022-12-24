using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostsByTag.Models;
using BlogApp.Domain.Entites;
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
            throw new NotFoundException(nameof(Tag), request.TagId);

        return await _dbContext.Posts.Where(p => p.Tags.Select(c => p.Id).Contains(request.TagId) &&
                                                 (!request.From.HasValue || p.CreationTime >= request.From) &&
                                                 (!request.To.HasValue || p.CreationTime <= request.To))
                                     .Skip((request.PageAmount.HasValue && request.Page.HasValue) ? request.Page.Value * request.PageAmount.Value : 0)
                                     .Take(request.PageAmount ?? int.MaxValue)
                                     .OrderBy(c => c.Id)
                                     .ProjectTo<GetPostsByTagDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);
    }
}
