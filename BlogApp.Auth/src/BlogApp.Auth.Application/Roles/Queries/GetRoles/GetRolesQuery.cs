using BlogApp.Auth.Application.Roles.Queries.GetRoles.Models;
using MediatR;

namespace BlogApp.Auth.Application.Roles.Queries.GetRoles;

//We don't need to pass here anything, only need this class for IRequest interface
public class GetRolesQuery : IRequest<List<GetRolesDto>> { }
