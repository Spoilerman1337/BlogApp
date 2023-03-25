using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;

namespace BlogApp.Auth.Application.Roles.Queries.GetRole.Models;

public class GetRoleDto : IMapFrom<AppRole>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppRole, GetRoleDto>();
    }
}
