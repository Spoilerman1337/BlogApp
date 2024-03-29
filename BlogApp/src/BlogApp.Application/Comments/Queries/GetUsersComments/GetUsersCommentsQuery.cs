﻿using BlogApp.Application.Comments.Queries.GetUsersComments.Models;
using BlogApp.Application.Common.Interfaces;
using MediatR;

namespace BlogApp.Application.Comments.Queries.GetUsersComments;

public class GetUsersCommentsQuery : IRequest<List<GetUsersCommentsDto>>, ICacheableQuery, IDateTimeFilterableQuery, IPaginatedQuery
{
    public Guid UserId { get; set; }

    public bool BypassCache { get; set; }
    public string CacheKey => $"GetCommentFromUser-{UserId}";
    public TimeSpan? SlidingExpiration { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int? PageAmount { get; set; }
    public int? Page { get; set; }
}
