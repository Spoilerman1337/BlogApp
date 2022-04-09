using BlogApp.Application.Comments.Commands.CreateComment;
using BlogApp.Application.Comments.Commands.DeleteComment;
using BlogApp.Application.Comments.Commands.UpdateComment;
using BlogApp.Application.Comments.Queries.GetComment;
using BlogApp.Application.Comments.Queries.GetComment.Models;
using BlogApp.Application.Comments.Queries.GetComments;
using BlogApp.Application.Comments.Queries.GetComments.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers;

public class CommentController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCommentDto>> GetComment(Guid id)
    {
        var query = new GetCommentQuery
        {
            Id = id,
            UserId = UserId,
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    [HttpGet]
    public async Task<ActionResult<List<GetCommentsDto>>> GetAllComments()
    {
        var query = new GetCommentsQuery
        {
            UserId = UserId,
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateComment(CreateCommentCommand command)
    {
        return await Sender.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateComment(Guid id, UpdateCommentCommand command)
    {
        if(id != command.Id)
        {
            return BadRequest();
        }

        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment(Guid id)
    {
        await Sender.Send(new DeleteCommentCommand
        {
            Id = id, 
            UserId = UserId,
        });

        return NoContent();
    }
}
