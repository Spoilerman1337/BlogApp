using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VotePosts.Commands.ChangeVotePost;

public class ChangeVotePostCommandHandler : IRequestHandler<ChangeVotePostCommand>
{
    private readonly IBlogDbContext _dbContext;

    public ChangeVotePostCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(ChangeVotePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.VotePosts.Where(c => c.PostId == request.PostId && c.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(VotePost), request.PostId, request.UserId);
        }

        entity.IsUpvoted = !entity.IsUpvoted;

        _dbContext.VotePosts.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
