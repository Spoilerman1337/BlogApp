using AutoMapper;
using BlogApp.Application.VotePosts.Commands.ChangeVotePost;
using BlogApp.Application.VotePosts.Commands.UnvotePost;
using BlogApp.Application.VotePosts.Commands.UpvotePost;
using BlogApp.Application.VotePosts.Commands.UpvotePost.Models;
using BlogApp.Application.VotePosts.Queries.GetPostsVotes;
using BlogApp.Application.VotePosts.Queries.GetPostsVotes.Models;
using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes;
using BlogApp.Application.VotePosts.Queries.GetUsersPostVotes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
public class VotePostController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public VotePostController(IMapper mapper) => _mapper = mapper;

    /// <summary>Gets all post's votes</summary>
    /// <remarks>
    /// Sample request:
    /// GET /votePost/post/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="postId">GUID ID of a post</param>
    /// <returns>Returns List of GetPostsVotesDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("post/{postId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<GetPostsVotesDto>>> GetPostsVotes([FromRoute] Guid postId)
    {
        var query = new GetPostsVotesQuery
        {
            PostId = postId
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets all user's post votes</summary>
    /// <remarks>
    /// Sample request:
    /// GET /votePost/user/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="userId">GUID ID of a user</param>
    /// <returns>Returns List of GetUsersPostVotesDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("user/{userId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<GetUsersPostVotesDto>>> GetUsersPostVotes([FromRoute] Guid userId)
    {
        var query = new GetUsersPostVotesQuery
        {
            UserId = userId
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Creates user's vote on post</summary>
    /// <remarks>
    /// Sample request:
    /// POST /votePost
    /// {
    ///     postId: "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///     isUpvoted: true,
    /// }
    /// </remarks>
    /// <param name="dto">UpvotePostDto object</param>
    /// <returns>Returns status of a new vote</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<bool>> UpvotePost([FromBody] UpvotePostDto dto)
    {
        var command = _mapper.Map<UpvotePostCommand>(dto);
        command.UserId = UserId;

        return await Sender.Send(command);
    }

    /// <summary>Removes user's vote on post</summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /votePost/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="postId">GUID ID of a post</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpDelete("{postId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UnvotePost([FromRoute] Guid postId)
    {
        await Sender.Send(new UnvotePostCommand
        {
            PostId = postId,
            UserId = UserId
        });

        return NoContent();
    }

    /// <summary>Changes user's vote on post on opposite</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /votePost/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="postId">GUID ID of a post</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut("{postId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> ChangeVotePost([FromRoute] Guid postId)
    {
        await Sender.Send(new ChangeVotePostCommand
        {
            PostId = postId,
            UserId = UserId
        });

        return NoContent();
    }
}
