using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.UpdatePost.Models;

public class UpdatePostDto : IMapFrom<UpdatePostCommand>
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Header { get; set; }
    public string Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePostDto, UpdatePostCommand>();
    }
}
