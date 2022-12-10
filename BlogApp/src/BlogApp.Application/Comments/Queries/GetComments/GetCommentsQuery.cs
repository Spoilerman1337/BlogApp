using BlogApp.Application.Comments.Queries.GetComments.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetComments;

public class GetCommentsQuery : IRequest<List<GetCommentsDto>>, ICacheableQuery
{
    public bool BypassCache { get; set; }
    public string CacheKey => $"GetComments";
    public TimeSpan? SlidingExpiration { get; set; }
}
