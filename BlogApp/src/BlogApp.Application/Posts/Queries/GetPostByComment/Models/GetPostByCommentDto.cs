using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Posts.Queries.GetPostByComment.Models;

public class GetPostByCommentDto : IMapFrom<Post>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Header { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
    public List<Guid> CommentIds { get; set; } = null!;
    public List<Guid> TagIds { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Post, GetPostByCommentDto>()
               .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.Tags.Select(c => c.Id)))
               .ForMember(dest => dest.CommentIds, opt => opt.MapFrom(src => src.Comments.Select(c => c.Id)));
    }
}
