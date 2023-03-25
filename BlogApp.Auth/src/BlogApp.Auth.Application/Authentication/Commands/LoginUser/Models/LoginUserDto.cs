using AutoMapper;
using BlogApp.Auth.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Authentication.Commands.LoginUser.Models;

public class LoginUserDto : IMapFrom<LoginUserCommand>
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required]
    public Guid Id { get; set; }    
    public string ReturnUrl { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginUserDto, LoginUserCommand>();
    }
}
