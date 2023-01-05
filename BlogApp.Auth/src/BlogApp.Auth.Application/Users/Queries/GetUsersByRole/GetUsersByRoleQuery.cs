using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Application.Users.Queries.GetUsersByRole.Models;
using MediatR;

namespace BlogApp.Auth.Application.Users.Queries.GetUsersByRole;

public class GetUsersByRoleQuery : IRequest<List<GetUsersByRoleDto>>, IPaginatedQuery
{
    public Guid RoleId { get; set; }

    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
