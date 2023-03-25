using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Comments.Commands.UpdateComment.Models;

public class UpdateCommentDto : IMapFrom<UpdateCommentCommand>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Text { get; set; } = null!;
    public Guid ParentCommentId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCommentDto, UpdateCommentCommand>();
    }
}
