using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Authentication.Commands.RegisterUser.Models;

public class RegisterUserDto : IMapFrom<RegisterUserCommand>
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    public string ReturnUrl { get; set; }
    public Guid RoleId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterUserDto, RegisterUserCommand>();
    }
}
