using BlogApp.Application.Tags.Queries.GetTag.Models;
using MediatR;

namespace BlogApp.Application.Tags.Queries.GetTag;

public class GetTagQuery : IRequest<GetTagDto>
{
    public Guid Id { get; set; }
}
