using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Tags.Commands.CreateTag;

public class CreateTagCommand : IRequest<Guid>
{
    [Required]
    public string TagName { get; set; }
}
