using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Commands.AttachTags;

public class AttachTagsCommandHandler : IRequestHandler<AttachTagsCommand>
{
    private readonly IBlogDbContext _dbContext;

    public AttachTagsCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(AttachTagsCommand request, CancellationToken cancellationToken)
    {
        var postEntity = await _dbContext.Posts.Include(t => t.Tags).ThenInclude(p => p.Posts).Where(p => p.Id == request.Id).SingleOrDefaultAsync(cancellationToken);
        var tagEntities = await _dbContext.Tags.Include(p => p.Posts).ThenInclude(t => t.Tags).Where(p => request.TagId.Contains(p.Id)).ToListAsync(cancellationToken);

        if (postEntity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }
        if (tagEntities.Count == 0)
        {
            throw new NotFoundException(nameof(Tag), request.TagId);
        }

        foreach (var tag in tagEntities)
        {
            postEntity.Tags.Add(tag);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
