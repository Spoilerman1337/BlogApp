using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Users.Commands.UpdateUser.Models;

public class UpdateUserDto : IMapFrom<UpdateUserCommand>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserDto, UpdateUserCommand>();
    }
}
