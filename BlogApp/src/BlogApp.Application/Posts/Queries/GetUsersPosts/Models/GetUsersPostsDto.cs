﻿using AutoMapper;
using BlogApp.Application.Common.Interfaces;
using BlogApp.Domain.Entites;

namespace BlogApp.Application.Posts.Queries.GetUsersPosts.Models;

public class GetUsersPostsDto : IMapFrom<Post>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Header { get; set; }
    public string Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastEdited { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Post, GetUsersPostsDto>();
    }
}
