using BlogApp.Application.Posts.Queries.GetUsersPosts.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts;

public class GetUsersPostsQuery : IRequest<List<GetUsersPostsDto>>
{
    public Guid UserId { get; set; }
}
