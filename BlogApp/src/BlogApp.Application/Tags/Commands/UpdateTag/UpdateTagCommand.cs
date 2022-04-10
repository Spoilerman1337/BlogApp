using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Tags.Commands.UpdateTag;

public class UpdateTagCommand : IRequest
{
    public Guid Id { get; set; }
    [Required]
    public string TagName { get; set; }
}


