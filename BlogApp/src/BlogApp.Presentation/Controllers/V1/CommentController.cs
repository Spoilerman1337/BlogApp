﻿using AutoMapper;
using BlogApp.Application.Comments.Commands.CreateComment;
using BlogApp.Application.Comments.Commands.CreateComment.Models;
using BlogApp.Application.Comments.Commands.DeleteComment;
using BlogApp.Application.Comments.Commands.UpdateComment;
using BlogApp.Application.Comments.Commands.UpdateComment.Models;
using BlogApp.Application.Comments.Queries.GetComment;
using BlogApp.Application.Comments.Queries.GetComment.Models;
using BlogApp.Application.Comments.Queries.GetComments;
using BlogApp.Application.Comments.Queries.GetComments.Models;
using BlogApp.Application.Comments.Queries.GetCommentsFromPost;
using BlogApp.Application.Comments.Queries.GetCommentsFromPost.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
public class CommentController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public CommentController(IMapper mapper) => _mapper = mapper;

    /// <summary>Gets comment by id</summary>
    /// <remarks>
    /// Sample request:
    /// GET /comment/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a comment</param>
    /// <returns>Returns GetCommentDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetCommentDto>> GetComment([FromRoute] Guid id)
    {
        var query = new GetCommentQuery
        {
            Id = id
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets all comments</summary>
    /// <remarks>
    /// Sample request:
    /// GET /comment
    /// </remarks>
    /// <returns>Returns List of GetCommentsDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<GetCommentsDto>>> GetAllComments()
    {
        var vm = await Sender.Send(new GetCommentsQuery());
        return Ok(vm);
    }

    /// <summary>Gets all post's comments</summary>
    /// <remarks>
    /// Sample request:
    /// GET /comment?postId=b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <returns>Returns List of GetCommentsFromPostDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<GetCommentsFromPostDto>>> GetAllCommentsFromPost([FromQuery] Guid postId)
    {
        var query = new GetCommentsFromPostQuery
        {
            PostId = postId,
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Creates user's comment</summary>
    /// <remarks>
    /// Sample request:
    /// POST /comment
    /// {
    ///     text: "string",
    /// }
    /// </remarks>
    /// <param name="dto">CreateCommentDto object</param>
    /// <returns>Returns GUID of a new comment</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> CreateComment([FromBody] CreateCommentDto dto)
    {
        var command = _mapper.Map<CreateCommentCommand>(dto);
        command.UserId = UserId;

        return await Sender.Send(command);
    }

    /// <summary>Updates user's comment</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /comment/b5c0a7ae-762d-445d-be15-b59232b19383
    /// {
    ///     text: "string",
    /// }
    /// </remarks>
    /// <param name="id">GUID ID of a comment</param>
    /// <param name="dto">UpdateCommentDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateComment([FromRoute] Guid id, [FromBody] UpdateCommentDto dto)
    {
        dto.Id = id;
        var command = _mapper.Map<UpdateCommentCommand>(dto);
        command.UserId = UserId;

        await Sender.Send(command);
        return NoContent();
    }

    /// <summary>Deletes user's comment</summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /comment/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a comment</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteComment([FromRoute] Guid id)
    {
        await Sender.Send(new DeleteCommentCommand
        {
            Id = id,
            UserId = UserId,
        });

        return NoContent();
    }
}