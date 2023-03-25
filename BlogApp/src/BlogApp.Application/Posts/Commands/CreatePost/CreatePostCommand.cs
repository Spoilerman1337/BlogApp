using MediatR;

namespace BlogApp.Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Header { get; set; } = null!;
    public string Text { get; set; } = null!;
}
