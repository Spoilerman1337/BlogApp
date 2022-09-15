using AutoMapper;
using BlogApp.Application.Posts.Commands.AttachTags;
using BlogApp.Application.Posts.Commands.AttachTags.Models;
using BlogApp.Application.Posts.Commands.CreatePost;
using BlogApp.Application.Posts.Commands.CreatePost.Models;
using BlogApp.Application.Posts.Commands.DeletePost;
using BlogApp.Application.Posts.Commands.DetachTags;
using BlogApp.Application.Posts.Commands.DetachTags.Models;
using BlogApp.Application.Posts.Commands.UpdatePost;
using BlogApp.Application.Posts.Commands.UpdatePost.Models;
using BlogApp.Application.Posts.Queries.GetPost;
using BlogApp.Application.Posts.Queries.GetPost.Models;
using BlogApp.Application.Posts.Queries.GetPostByComment;
using BlogApp.Application.Posts.Queries.GetPostByComment.Models;
using BlogApp.Application.Posts.Queries.GetPosts;
using BlogApp.Application.Posts.Queries.GetPosts.Models;
using BlogApp.Application.Posts.Queries.GetPostsByTag;
using BlogApp.Application.Posts.Queries.GetPostsByTag.Models;
using BlogApp.Application.Posts.Queries.GetUsersPosts;
using BlogApp.Application.Posts.Queries.GetUsersPosts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
public class PostController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public PostController(IMapper mapper) => _mapper = mapper;

    /// <summary>Gets post by id</summary>
    /// <remarks>
    /// Sample request:
    /// GET /post/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a post</param>
    /// <returns>Returns GetCommentDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPostDto>> GetPost([FromRoute] Guid id)
    {
        var query = new GetPostQuery
        {
            Id = id,
            BypassCache = false
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets all posts</summary>
    /// <remarks>
    /// Sample request:
    /// GET /post
    /// </remarks>
    /// <returns>Returns GetPostsDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPostsDto>> GetAllPosts()
    {
        var query = new GetPostsQuery()
        {
            BypassCache = false
        };
        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets user's post by id</summary>
    /// <remarks>
    /// Sample request:
    /// GET /post/user/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="userId">GUID ID of a user</param>
    /// <returns>Returns GetCommentDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("user/{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetUsersPostsDto>> GetUsersPosts([FromRoute] Guid userId)
    {
        var query = new GetUsersPostsQuery
        {
            UserId = userId,
            BypassCache = false
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets post where comment is written</summary>
    /// <remarks>
    /// Sample request:
    /// GET /post/comment/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="commentId">GUID ID of a user</param>
    /// <returns>Returns GetPostByCommentDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("comment/{commentId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPostByCommentDto>> GetPostByComment([FromRoute] Guid commentId)
    {
        var query = new GetPostByCommentQuery
        {
            CommentId = commentId,
            BypassCache = false
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets posts where tag is used</summary>
    /// <remarks>
    /// Sample request:
    /// GET /post/tag/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="tagId">GUID ID of a user</param>
    /// <returns>Returns GetPostsByTagDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("tag/{tagId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetPostsByTagDto>> GetPostsByTag([FromRoute] Guid tagId)
    {
        var query = new GetPostsByTagQuery
        {
            TagId = tagId,
            BypassCache = false
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
    /// <param name="dto">CreatePostDto object</param>
    /// <returns>Returns GUID of a new post</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPost]
    [Authorize]
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
    /// <param name="dto">UpdatePostDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdatePost([FromRoute] Guid id, [FromBody] UpdatePostDto dto)
    {
        dto.Id = id;
        var command = _mapper.Map<UpdatePostCommand>(dto);
        command.UserId = UserId;

        await Sender.Send(command);
        return NoContent();
    }

    /// <summary>Attaches tags to post</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /post
    /// {
    ///     id: "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///     tagIds: [
    ///         "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///         "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///         "b5c0a7ae-762d-445d-be15-b59232b19383"
    ///     ]
    /// }
    /// </remarks>
    /// <param name="dto">AttachTagsDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> AttachTagToPost([FromBody] AttachTagsDto dto)
    {
        var command = _mapper.Map<AttachTagsCommand>(dto);

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
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeletePost([FromRoute] Guid id)
    {
        await Sender.Send(new DeletePostCommand
        {
            Id = id,
        });

        return NoContent();
    }

    /// <summary>Detaches tags from post</summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /post
    /// {
    ///     id: "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///     tagIds: [
    ///         "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///         "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///         "b5c0a7ae-762d-445d-be15-b59232b19383"
    ///     ]
    /// }
    /// </remarks>
    /// <param name="dto">DetachTagsDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DetachTagToPost([FromBody] DetachTagsDto dto)
    {
        var command = _mapper.Map<DetachTagsCommand>(dto);

        await Sender.Send(command);
        return NoContent();
    }
}
