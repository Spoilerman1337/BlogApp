using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Users.Commands.LoginUser.Models;

public class LoginUserDto : IMapFrom<LoginUserCommand>
{
    [Required]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    public Guid Id { get; set; }    
    public string ReturnUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginUserDto, LoginUserCommand>();
    }
}
