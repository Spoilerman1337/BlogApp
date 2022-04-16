using MediatR;

namespace BlogApp.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string Header { get; set; }
    public string Text { get; set; }
}
