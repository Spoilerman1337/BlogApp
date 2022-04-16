using AutoMapper;
using BlogApp.Application.Posts.Commands.CreatePost;
using BlogApp.Application.Posts.Commands.CreatePost.Models;
using BlogApp.Application.Posts.Commands.DeletePost;
using BlogApp.Application.Posts.Commands.UpdatePost;
using BlogApp.Application.Posts.Commands.UpdatePost.Models;
using BlogApp.Application.Posts.Queries.GetPost;
using BlogApp.Application.Posts.Queries.GetPost.Models;
using BlogApp.Application.Posts.Queries.GetPosts;
using BlogApp.Application.Posts.Queries.GetPosts.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
public class PostController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public PostController(IMapper mapper) => _mapper = mapper;

    /// <summary>Gets user's post by id</summary>
    /// <remarks>
    /// Sample request:
    /// GET /post/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a post</param>
    /// <returns>Returns GetCommentDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>Gets all user's posts</summary>
    /// <remarks>
    /// Sample request:
    /// GET /post
    /// </remarks>
    /// <returns>Returns List of GetPostsDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPostsDto>> GetAllPosts()
    {
        var query = new GetPostsQuery
        {
            UserId = UserId,
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Creates user's post</summary>
    /// <remarks>
    /// Sample request:
    /// POST /post
    /// {
    ///     header: "string",
    ///     text: "string",
    /// }
    /// </remarks>
    /// <param name="command">CreatePostDto object</param>
    /// <returns>Returns GUID of a new post</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> CreatePost([FromBody] CreatePostDto dto)
    {
        var command = _mapper.Map<CreatePostCommand>(dto);
        command.UserId = UserId;

        return await Sender.Send(command);
    }

    /// <summary>Updates user's post</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /post/b5c0a7ae-762d-445d-be15-b59232b19383
    /// {
    ///     header: "string",
    ///     text: "string",
    /// }
    /// </remarks>
    /// <param name="id">GUID ID of a post</param>
    /// <param name="command">UpdatePostDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdatePost([FromRoute] Guid id, [FromBody] UpdatePostDto dto)
    {
        dto.Id = id;
        var command = _mapper.Map<UpdatePostCommand>(dto);
        command.UserId = UserId;

        await Sender.Send(command);
        return NoContent();
    }

    /// <summary>Deletes user's post</summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /post/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a post</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
