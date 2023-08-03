using BlogApp.Auth.Application.Users.Queries.GetUserByName.Models;
using MediatR;

namespace BlogApp.Auth.Application.Users.Queries.GetUserByName;

public class GetUserByNameQuery : IRequest<GetUserByNameDto>
{
    public string UserName { get; set; }
}
