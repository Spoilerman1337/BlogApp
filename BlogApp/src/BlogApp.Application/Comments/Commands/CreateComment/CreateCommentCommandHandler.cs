using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;

namespace BlogApp.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly IBlogDbContext _dbContext;

    public CreateCommentCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Text = request.Text,
            CreationTime = DateTime.Now,
            LastEdited = null,
            ParentComment = request.ParentComment
        };

        await _dbContext.Comments.AddAsync(comment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return comment.Id;
    }
}
