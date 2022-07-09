using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{
    private readonly IBlogDbContext _dbContext;

    public UpdateCommentCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Comments.Where(c => c.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Comment), request.Id);
        }

        entity.Text = request.Text;
        entity.LastEdited = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
