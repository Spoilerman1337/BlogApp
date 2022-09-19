using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Application.VoteComments.Commands.UpvoteComment.Models;

public class UpvoteCommentDto : IMapFrom<UpvoteCommentCommand>
{
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public bool IsUpvoted { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpvoteCommentDto, UpvoteCommentCommand>();
    }
}
