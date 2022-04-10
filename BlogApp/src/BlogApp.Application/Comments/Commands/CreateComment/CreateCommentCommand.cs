using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Comments.Commands.CreateComment;

public class CreateCommentCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    [Required]
    public string Text { get; set; }
}
