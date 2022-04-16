using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.Posts.Commands.CreatePost.Models;

public class CreatePostDto : IMapFrom<CreatePostCommand>
{
    [Required]
    public string Header { get; set; }
    public string Text { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePostDto, CreatePostCommand>();
    }
}
