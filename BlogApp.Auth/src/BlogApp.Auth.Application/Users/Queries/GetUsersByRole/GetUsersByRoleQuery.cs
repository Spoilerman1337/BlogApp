using BlogApp.Auth.Application.Users.Queries.GetUsersByRole.Models;
using MediatR;

namespace BlogApp.Auth.Application.Users.Queries.GetUsersByRole;

public class GetUsersByRoleQuery : IRequest<List<GetUsersByRoleDto>>
{
    public Guid RoleId { get; set; }
}
