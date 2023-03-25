using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.DetachTags.Models;

public class DetachTagsDto : IMapFrom<DetachTagsCommand>
{
    [Required]
    public List<Guid> TagIds { get; set; } = null!;
    [Required]
    public Guid Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DetachTagsDto, DetachTagsCommand>();
    }
}
