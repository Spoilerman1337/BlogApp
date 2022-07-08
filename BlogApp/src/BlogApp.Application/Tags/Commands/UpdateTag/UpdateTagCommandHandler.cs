using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Tags.Commands.UpdateTag;

public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand>
{
    private readonly IBlogDbContext _dbContext;

    public UpdateTagCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Tags.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Tag), request.Id);
        }

        entity.TagName = request.TagName;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
