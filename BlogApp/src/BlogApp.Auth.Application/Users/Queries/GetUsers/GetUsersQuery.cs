using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using MediatR;

namespace BlogApp.Auth.Application.Users.Queries.GetUsers;

//We don't need to pass here anything, only need this class for IRequest interface
public class GetUsersQuery : IRequest<List<GetUsersDto>> { }
