using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.VotePosts.Commands.UpvotePost.Models;

public class UpvotePostDto : IMapFrom<UpvotePostDto>
{
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public bool IsUpvoted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpvotePostDto, UpvotePostCommand>();
    }
}
