using BlogApp.Application.Posts.Queries.GetPostByTag.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPostByTag;

public class GetPostsByTagQuery : IRequest<List<GetPostsByTagDto>>
{
    public Guid TagId { get; set; }
}
