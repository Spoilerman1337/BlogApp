using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Tags.Queries.GetTags.Models;

public class GetTagsDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string TagName { get; set; }
}
