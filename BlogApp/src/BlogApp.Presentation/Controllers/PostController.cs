using BlogApp.Application.Posts.Commands.CreatePost;
using BlogApp.Application.Posts.Commands.DeletePost;
using BlogApp.Application.Posts.Commands.UpdatePost;
using BlogApp.Application.Posts.Queries.GetPost;
using BlogApp.Application.Posts.Queries.GetPost.Models;
using BlogApp.Application.Posts.Queries.GetPosts;
using BlogApp.Application.Posts.Queries.GetPosts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers;

public class PostController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetPostDto>> GetPost(Guid id)
    {
        var query = new GetPostQuery
        {
            Id = id,
            UserId = UserId,
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }
    
    [HttpGet]
    public async Task<ActionResult<GetPostsDto>> GetAllPosts()
    {
        var query = new GetPostsQuery
        {
            UserId = UserId,
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreatePost(CreatePostCommand command)
    {
        return await Sender.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePost(Guid id, UpdatePostCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePost(Guid id)
    {
        await Sender.Send(new DeletePostCommand
        {
            Id = id,
            UserId = UserId,
        });

        return NoContent();
    }
}
