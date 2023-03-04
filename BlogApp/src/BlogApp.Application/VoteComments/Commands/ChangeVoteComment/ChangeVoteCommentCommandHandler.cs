using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VoteComments.Commands.ChangeVoteComment;

public class ChangeVoteCommentCommandHandler : IRequestHandler<ChangeVoteCommentCommand>
{
    private readonly IBlogDbContext _dbContext;

    public ChangeVoteCommentCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(ChangeVoteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.VoteComments.Where(c => c.CommentId == request.CommentId && c.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(VoteComment), request.CommentId, request.UserId);
        }

        entity.IsUpvoted = !entity.IsUpvoted;

        _dbContext.VoteComments.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
