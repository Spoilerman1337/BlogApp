using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.AttachTags.Models;

public class AttachTagsDto : IMapFrom<AttachTagsCommand>
{
    [Required]
    public List<Guid> TagIds { get; set; }
    [Required]
    public Guid Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AttachTagsDto, AttachTagsCommand>();
    }
}
