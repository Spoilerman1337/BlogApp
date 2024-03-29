﻿using BlogApp.Application.Common.Exceptions;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly IBlogDbContext _dbContext;

    public UpdatePostCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Posts.Where(c => c.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        if (entity == null || entity.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }

        entity.Text = request.Text;
        entity.Header = request.Header;
        entity.LastEdited = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
