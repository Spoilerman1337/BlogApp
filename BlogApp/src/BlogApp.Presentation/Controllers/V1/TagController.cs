using AutoMapper;
using BlogApp.Application.Tags.Commands.CreateTag;
using BlogApp.Application.Tags.Commands.CreateTag.Models;
using BlogApp.Application.Tags.Commands.DeleteTag;
using BlogApp.Application.Tags.Commands.UpdateTag;
using BlogApp.Application.Tags.Commands.UpdateTag.Models;
using BlogApp.Application.Tags.Queries.GetPostsTags;
using BlogApp.Application.Tags.Queries.GetPostsTags.Models;
using BlogApp.Application.Tags.Queries.GetTag;
using BlogApp.Application.Tags.Queries.GetTag.Models;
using BlogApp.Application.Tags.Queries.GetTags;
using BlogApp.Application.Tags.Queries.GetTags.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
public class TagController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public TagController(IMapper mapper) => _mapper = mapper;

    /// <summary>Gets tag by id</summary>
    /// <remarks>
    /// Sample request:
    /// GET /tag/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a tag</param>
    /// <returns>Returns GetTagDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetTagDto>> GetTag([FromRoute] Guid id)
    {
        var query = new GetTagQuery
        {
            Id = id,
            BypassCache = false
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets all tags</summary>
    /// <remarks>
    /// Sample request:
    /// GET /tag
    /// </remarks>
    /// <returns>Returns GetTagsDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetTagsDto>> GetAllTags()
    {
        var query = new GetTagsQuery()
        {
            BypassCache = false
        };
        var vm = await Sender.Send(query);

        return Ok(vm);
    }

    /// <summary>Gets all posts tags</summary>
    /// <remarks>
    /// Sample request:
    /// GET /tag/post/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="postId">GUID ID of a tag</param>
    /// <returns>Returns GetTagDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("post/{postId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPostsTagsDto>> GetPostsTags([FromRoute] Guid postId)
    {
        var query = new GetPostsTagsQuery
        {
            PostId = postId,
            BypassCache = false
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Creates tag</summary>
    /// <remarks>
    /// Sample request:
    /// POST /tag
    /// {
    ///     tagname: "string",
    /// }
    /// </remarks>
    /// <param name="dto">CreateTagDto object</param>
    /// <returns>Returns GUID ID of a new tag</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> CreateTag([FromBody] CreateTagDto dto)
    {
        var command = _mapper.Map<CreateTagCommand>(dto);

        return await Sender.Send(command);
    }

    /// <summary>Updates tag</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /tag/b5c0a7ae-762d-445d-be15-b59232b19383
    /// {
    ///     tagname: "string",
    /// }
    /// </remarks>
    /// <param name="id">GUID ID of a tag</param>
    /// <param name="dto">UpdateTagDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateTag([FromRoute] Guid id, [FromBody] UpdateTagDto dto)
    {
        dto.Id = id;
        var command = _mapper.Map<UpdateTagCommand>(dto);

        await Sender.Send(command);
        return NoContent();
    }

    /// <summary>Deletes tag</summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /tag/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a tag</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteTag([FromRoute] Guid id)
    {
        await Sender.Send(new DeleteTagCommand
        {
            Id = id,
        });

        return NoContent();
    }
}
