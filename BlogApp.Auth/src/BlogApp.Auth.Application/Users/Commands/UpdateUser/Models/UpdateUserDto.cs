using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Users.Commands.UpdateUser.Models;

public class UpdateUserDto : IMapFrom<UpdateUserCommand>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserDto, UpdateUserCommand>();
    }
}
