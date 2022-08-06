using BlogApp.Auth.Application.Users.Queries.GetUser.Models;
using MediatR;

namespace BlogApp.Auth.Application.Users.Queries.GetUser;

public class GetUserQuery : IRequest<GetUserDto>
{
    public Guid Id { get; set; }
}
