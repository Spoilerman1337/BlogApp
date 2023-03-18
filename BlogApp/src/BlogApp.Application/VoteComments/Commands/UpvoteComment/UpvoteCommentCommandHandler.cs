using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.VoteComments.Commands.UpvoteComment;

public class UpvoteCommentCommandHandler : IRequestHandler<UpvoteCommentCommand, bool>
{
    private readonly IBlogDbContext _dbContext;

    public UpvoteCommentCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<bool> Handle(UpvoteCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);

        if (comment == null)
        {
            throw new NotFoundException(nameof(Comment), request.CommentId);
        }

        var entity = new VoteComment
        {
            CommentId = request.CommentId,
            UserId = request.UserId,
            IsUpvoted = request.IsUpvoted
        };

        await _dbContext.VoteComments.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.IsUpvoted;
    }
}
