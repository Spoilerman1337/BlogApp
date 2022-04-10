using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.UpdatePost;

public class UpdatePostCommand : IRequest
{
    public Guid UserId { get; set; }
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Header { get; set; }
    public string Text { get; set; }
}
