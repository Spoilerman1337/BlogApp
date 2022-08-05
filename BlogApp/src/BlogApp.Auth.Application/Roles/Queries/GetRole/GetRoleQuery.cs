using BlogApp.Auth.Application.Roles.Queries.GetRole.Models;
using MediatR;

namespace BlogApp.Auth.Application.Roles.Queries.GetRole;

public class GetRoleQuery : IRequest<GetRoleDto>
{
    public Guid Id { get; set; }
}
