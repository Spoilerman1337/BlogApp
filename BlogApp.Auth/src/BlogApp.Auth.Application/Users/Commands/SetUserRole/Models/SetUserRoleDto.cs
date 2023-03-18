using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Users.Commands.SetUserRole.Models;

public class SetUserRoleDto : IMapFrom<SetUserRoleCommand>
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid RoleId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetUserRoleDto, SetUserRoleCommand>();
    }
}
