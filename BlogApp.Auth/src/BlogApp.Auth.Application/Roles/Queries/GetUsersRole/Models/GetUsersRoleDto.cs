﻿using BlogApp.Auth.Domain.Entities;
using Mapster;

namespace BlogApp.Auth.Application.Roles.Queries.GetUsersRole.Models;

public class GetUsersRoleDto : IMapFrom<RoleEntity>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<RoleEntity, GetUsersRoleDto>();
    }
}
