using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Tags.Queries.GetPostsTags.Models;
using MediatR;

namespace BlogApp.Application.Tags.Queries.GetPostsTags;

public class GetPostsTagsQuery : IRequest<List<GetPostsTagsDto>>, ICacheableQuery
{
    public Guid PostId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetPostsTags-{PostId}";
    public TimeSpan? SlidingExpiration { get; set; }
}
