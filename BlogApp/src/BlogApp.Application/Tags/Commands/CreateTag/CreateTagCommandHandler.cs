using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;

namespace BlogApp.Application.Tags.Commands.CreateTag;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly IBlogDbContext _dbContext;

    public CreateTagCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag
        {
            Id = Guid.NewGuid(),
            TagName = request.TagName
        };

        await _dbContext.Tags.AddAsync(tag, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return tag.Id;
    }
}
