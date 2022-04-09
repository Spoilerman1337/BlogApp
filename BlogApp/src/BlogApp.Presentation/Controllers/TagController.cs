using BlogApp.Application.Tags.Commands.CreateTag;
using BlogApp.Application.Tags.Commands.DeleteTag;
using BlogApp.Application.Tags.Commands.UpdateTag;
using BlogApp.Application.Tags.Queries.GetTag;
using BlogApp.Application.Tags.Queries.GetTag.Models;
using BlogApp.Application.Tags.Queries.GetTags;
using BlogApp.Application.Tags.Queries.GetTags.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers;

public class TagController : ApiControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GetTagDto>> GetTag(Guid id)
    {
        var query = new GetTagQuery
        {
            Id = id,
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    [HttpGet]
    public async Task<ActionResult<GetTagsDto>> GetAllTags()
    {
        var vm = await Sender.Send(new GetTagsQuery());

        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTag(CreateTagCommand command)
    {
        return await Sender.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTag(Guid id, UpdateTagCommand command)
    {
        if(id != command.Id)
        {
            return BadRequest();
        }

        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTag(Guid id)
    {
        await Sender.Send(new DeleteTagCommand
        {
            Id = id,
        });

        return NoContent();
    }
}
