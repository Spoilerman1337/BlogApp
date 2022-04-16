using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Tags.Queries.GetTags.Models;

public class GetTagsDto : IMapFrom<Tag>
{
    public Guid Id { get; set; }
    public string TagName { get; set; }
    public List<Guid> PostIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, GetTagsDto>()
               .ForMember(dest => dest.PostIds, opt => opt.MapFrom(src => src.Posts.Select(c => c.Id)));
    }
}
