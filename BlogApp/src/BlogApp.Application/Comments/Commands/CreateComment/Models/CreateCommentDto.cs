using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Comments.Commands.CreateComment.Models;

public class CreateCommentDto : IMapFrom<CreateCommentCommand>
{
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    public Guid ParentCommentId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCommentDto, CreateCommentCommand>();
    }
}
