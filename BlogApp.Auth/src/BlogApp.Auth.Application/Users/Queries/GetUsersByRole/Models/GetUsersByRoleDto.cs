using BlogApp.Auth.Domain.Entities;
using Mapster;

namespace BlogApp.Auth.Application.Users.Queries.GetUsersByRole.Models;

public class GetUsersByRoleDto : IMapFrom<UserEntity>
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Patronymic { get; set; } = null!;

    public void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<UserEntity, GetUsersByRoleDto>();
    }
}
