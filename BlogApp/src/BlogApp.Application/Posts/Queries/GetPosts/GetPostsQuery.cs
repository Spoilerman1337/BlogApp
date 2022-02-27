using BlogApp.Application.Posts.Queries.GetPosts.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPosts;

public class GetPostsQuery : IRequest<List<GetPostsDto>>
{
    public Guid UserId { get; set; }
}
