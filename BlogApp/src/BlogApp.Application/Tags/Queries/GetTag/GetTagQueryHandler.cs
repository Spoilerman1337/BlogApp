using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetTag.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Tags.Queries.GetTag;

public class GetTagQueryHandler : IRequestHandler<GetTagQuery, GetTagDto>
{
    private readonly IBlogDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetTagQueryHandler(IBlogDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

    public async Task<GetTagDto> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await _dbContext.Tags.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        return _mapper.Map<GetTagDto>(tag);
    }
}
