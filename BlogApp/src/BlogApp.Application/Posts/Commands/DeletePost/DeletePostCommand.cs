using MediatR;

namespace BlogApp.Application.Posts.Commands.DeletePost;

public class DeletePostCommand : IRequest
{
    public Guid Id { get; set; }
}
