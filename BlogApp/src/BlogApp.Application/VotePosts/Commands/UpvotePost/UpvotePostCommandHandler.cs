using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VotePosts.Commands.UpvotePost;

public class UpvotePostCommandHandler : IRequestHandler<UpvotePostCommand, bool>
{
    private readonly IBlogDbContext _dbContext;

    public UpvotePostCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> Handle(UpvotePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(c => c.Id == request.PostId, cancellationToken);

        if (post == null)
        {
            throw new NotFoundException(nameof(Post), request.PostId);
        }

        var entity = new VotePost
        {
            PostId = request.PostId,
            UserId = request.UserId,
            IsUpvoted = request.IsUpvoted
        };

        await _dbContext.VotePosts.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.IsUpvoted;
    }
}
