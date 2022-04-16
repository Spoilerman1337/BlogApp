using BlogApp.Application.Posts.Queries.GetPosts.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPosts;

//We don't need to pass here anything, only need this class for IRequest interface
public class GetPostsQuery : IRequest<List<GetPostsDto>> { }
