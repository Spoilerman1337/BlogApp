using MediatR;

namespace BlogApp.Application.Posts.Commands.DeletePost;

public class DeletePostCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}
