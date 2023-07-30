using Mapster;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Auth.Application.Users.Commands.SetUserRole.Models;

public class SetUserRoleDto : IMapFrom<SetUserRoleCommand>
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid RoleId { get; set; }

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<SetUserRoleDto, SetUserRoleCommand>();
    }
}
