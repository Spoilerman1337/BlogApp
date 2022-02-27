using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Tags.Queries.GetTag.Models;

public class GetTagDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string TagName { get; set; }
}
