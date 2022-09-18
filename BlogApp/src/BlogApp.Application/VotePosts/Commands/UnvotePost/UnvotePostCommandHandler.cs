using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VotePosts.Commands.UnvotePost;

public class UnvotePostCommandHandler : IRequestHandler<UnvotePostCommand>
{
    private readonly IBlogDbContext _dbContext;

    public UnvotePostCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<Unit> Handle(UnvotePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.VotePosts.Where(c => c.PostId == request.PostId && c.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(VotePost), request.PostId, request.UserId);
        }

        _dbContext.VotePosts.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
