using BlogApp.Application.Comments.Queries.GetComments.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetComments;

public class GetCommentsQuery : IRequest<List<GetCommentsDto>>, ICacheableQuery, IDateTimeFilterableQuery, IPaginatedQuery
{
    public bool BypassCache { get; set; }
    public string CacheKey => $"GetComments";
    public TimeSpan? SlidingExpiration { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
