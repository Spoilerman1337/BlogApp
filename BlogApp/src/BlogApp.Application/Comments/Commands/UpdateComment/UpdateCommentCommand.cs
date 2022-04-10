using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : IRequest
{
    [Required]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [Required]
    public string Text { get; set; }
}
