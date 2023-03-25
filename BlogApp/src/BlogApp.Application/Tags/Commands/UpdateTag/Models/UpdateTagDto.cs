using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Tags.Commands.UpdateTag.Models;

public class UpdateTagDto
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string TagName { get; set; } = null!;
}
