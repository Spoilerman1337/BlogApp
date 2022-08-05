using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Domain.Entities;

namespace BlogApp.Auth.Application.Users.Queries.GetUsers.Models;

public class GetUsersDto : IMapFrom<AppUser>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AppUser, GetUsersDto>();
    }
}