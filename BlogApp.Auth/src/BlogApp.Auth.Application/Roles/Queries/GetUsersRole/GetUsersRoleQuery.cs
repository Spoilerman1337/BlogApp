using BlogApp.Auth.Application.Roles.Queries.GetUsersRole.Models;
using MediatR;

namespace BlogApp.Auth.Application.Roles.Queries.GetUsersRole;

public class GetUsersRoleQuery : IRequest<GetUsersRoleDto>
{
    public Guid UserId { get; set; }
}
