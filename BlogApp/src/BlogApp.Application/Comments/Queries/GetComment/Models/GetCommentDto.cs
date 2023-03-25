using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Comments.Queries.GetComment.Models;

public class GetCommentDto : IMapFrom<Comment>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
    public Guid? ParentCommentId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Comment, GetCommentDto>();
    }
}
