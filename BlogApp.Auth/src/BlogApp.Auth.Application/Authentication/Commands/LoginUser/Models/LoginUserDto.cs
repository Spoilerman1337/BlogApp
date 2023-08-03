using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Authentication.Commands.LoginUser.Models;

public class LoginUserDto : IMapFrom<LoginUserCommand>
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    public string ReturnUrl { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<LoginUserDto, LoginUserCommand>();
    }
}
