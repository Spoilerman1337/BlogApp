﻿using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Comments.Commands.DeleteComment;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly IBlogDbContext _dbContext;

    public DeleteCommentCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Comments.Where(c => c.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Comment), request.Id);
        }

        _dbContext.Comments.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
