using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Tags.Commands.CreateTag.Models;

public class CreateTagDto : IMapFrom<CreateTagCommand>
{
    [Required]
    public string TagName { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateTagDto, CreateTagCommand>();
    }
}
