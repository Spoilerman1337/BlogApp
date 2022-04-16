using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Posts.Queries.GetPost.Models;

public class GetPostDto : IMapFrom<Post>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Header { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
    public List<Guid> CommentIds { get; set; }
    public List<Guid> TagIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Post, GetPostDto>()
               .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.Tags.Select(c => c.Id)))
               .ForMember(dest => dest.CommentIds, opt => opt.MapFrom(src => src.Comments.Select(c => c.Id)));
    }
}
