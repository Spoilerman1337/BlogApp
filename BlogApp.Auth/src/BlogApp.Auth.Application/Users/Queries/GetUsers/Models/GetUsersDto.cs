using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;

namespace BlogApp.Auth.Application.Users.Queries.GetUsers.Models;

public class GetUsersDto : IMapFrom<AppUser>
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, GetUsersDto>();
    }
}