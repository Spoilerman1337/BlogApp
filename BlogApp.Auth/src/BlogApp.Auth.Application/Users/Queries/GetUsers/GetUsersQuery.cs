using BlogApp.Auth.Application.Common.Interfaces;
using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using MediatR;

namespace BlogApp.Auth.Application.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<List<GetUsersDto>>, IPaginatedQuery
{
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
