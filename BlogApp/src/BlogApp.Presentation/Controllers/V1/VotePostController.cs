using AutoMapper;
using BlogApp.Application.VotePosts.Commands.UpvotePost;
using BlogApp.Application.VotePosts.Commands.UpvotePost.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
public class VotePostController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public VotePostController(IMapper mapper) => _mapper = mapper;

    /// <summary>Creates user's vote on post</summary>
    /// <remarks>
    /// Sample request:
    /// POST /post
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
}
