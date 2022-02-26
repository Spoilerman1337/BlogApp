using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;
using MediatR;

namespace BlogApp.Application.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid>
{
    private readonly IBlogDbContext _dbContext;

    public CreatePostCommandHandler(IBlogDbContext dbContext) => _dbContext = dbContext;

    public async Task<Guid> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            Header = request.Header,
            Text = request.Text,
            CreationTime = DateTime.Now,
            LastEdited = null
        };

        await _dbContext.Posts.AddAsync(post, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}
