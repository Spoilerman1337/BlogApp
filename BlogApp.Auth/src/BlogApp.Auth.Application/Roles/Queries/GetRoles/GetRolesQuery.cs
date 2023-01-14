using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Application.Roles.Queries.GetRoles.Models;
using MediatR;

namespace BlogApp.Auth.Application.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<List<GetRolesDto>>, IPaginatedQuery
{
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
