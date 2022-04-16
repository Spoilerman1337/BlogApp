using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Comments.Queries.GetUsersComments.Models;

public class GetUsersCommentsDto : IMapFrom<Comment>
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }
    public Guid? ParentComment { get; set; }
    public Guid PostId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Comment, GetUsersCommentsDto>();
    }
}
