using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VoteComments.Commands.UnvoteComment;

public class UnvoteCommentCommandHandler : IRequestHandler<UnvoteCommentCommand>
{
    private readonly IBlogDbContext _dbContext;

    public UnvoteCommentCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<Unit> Handle(UnvoteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.VoteComments.Where(c => c.CommentId == request.CommentId && c.UserId == request.UserId).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(VoteComment), request.CommentId, request.UserId);
        }

        _dbContext.VoteComments.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
