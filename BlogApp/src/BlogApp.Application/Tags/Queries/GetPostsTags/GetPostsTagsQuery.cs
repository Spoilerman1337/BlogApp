using BlogApp.Application.Tags.Queries.GetPostsTags.Models;
using MediatR;

namespace BlogApp.Application.Tags.Queries.GetPostsTags;

public class GetPostsTagsQuery : IRequest<List<GetPostsTagsDto>>
{
    public Guid PostId { get; set; }
}
