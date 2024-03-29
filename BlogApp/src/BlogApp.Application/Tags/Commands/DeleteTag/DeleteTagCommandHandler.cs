﻿using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Tags.Commands.DeleteTag;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
{
    private readonly IBlogDbContext _dbContext;

    public DeleteTagCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Tags.Where(c => c.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Tag), request.Id);
        }

        _dbContext.Tags.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
