﻿using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Roles.Commands.CreateRole.Models;

public class CreateRoleDto : IMapFrom<CreateRoleCommand>
{
    [Required]
    public string Name { get; set; } = null!;
}
