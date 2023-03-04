using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Commands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IBlogDbContext _dbContext;

    public DeletePostCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Posts.Where(c => c.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }

        _dbContext.Posts.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
