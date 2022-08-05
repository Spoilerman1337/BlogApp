using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Application.Roles.Commands.CreateRole;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Roles.Commands.UpdateRole.Models;

public class UpdateRoleDto : IMapFrom<CreateRoleCommand>
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
}
