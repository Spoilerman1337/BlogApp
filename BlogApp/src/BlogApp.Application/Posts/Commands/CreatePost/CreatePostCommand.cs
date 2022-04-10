using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    [Required]
    public string Header { get; set; }
    public string Text { get; set; }
}
