using BlogApp.Application.Posts.Queries.GetPost.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPost;

public class GetPostQuery : IRequest<GetPostDto>
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}
