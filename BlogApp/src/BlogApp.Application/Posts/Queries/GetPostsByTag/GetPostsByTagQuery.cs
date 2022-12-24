using BlogApp.Application.Common.Interfaces;
using BlogApp.Application.Posts.Queries.GetPostsByTag.Models;
using MediatR;

namespace BlogApp.Application.Posts.Queries.GetPostsByTag;

public class GetPostsByTagQuery : IRequest<List<GetPostsByTagDto>>, ICacheableQuery, ISortableQuery
{
    public Guid TagId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetPostsByTag-{TagId}";
    public TimeSpan? SlidingExpiration { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
