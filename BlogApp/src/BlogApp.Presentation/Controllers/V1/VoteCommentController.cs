using AutoMapper;
using BlogApp.Application.VoteComments.Commands.ChangeVoteComment;
using BlogApp.Application.VoteComments.Commands.UnvoteComment;
using BlogApp.Application.VoteComments.Commands.UpvoteComment;
using BlogApp.Application.VoteComments.Commands.UpvoteComment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
public class VoteCommentController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public VoteCommentController(IMapper mapper) => _mapper = mapper;

    /// <summary>Creates user's vote on comment</summary>
    /// <remarks>
    /// Sample request:
    /// POST /voteComment
    /// {
    ///     commentId: "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///     isUpvoted: true,
    /// }
    /// </remarks>
    /// <param name="dto">UpvoteCommentDto object</param>
    /// <returns>Returns status of a new vote</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<bool>> UpvoteComment([FromBody] UpvoteCommentDto dto)
    {
        var command = _mapper.Map<UpvoteCommentCommand>(dto);
        command.UserId = UserId;

        return await Sender.Send(command);
    }

    /// <summary>Removes user's vote on comment</summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /voteComment/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="commentId">GUID ID of a comment</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpDelete("{commentId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UnvoteComment([FromRoute] Guid commentId)
    {
        await Sender.Send(new UnvoteCommentCommand
        {
            CommentId = commentId,
            UserId = UserId
        });

        return NoContent();
    }

    /// <summary>Changes user's vote on comment on opposite</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /voteComment/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="commentId">GUID ID of a commentId</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut("{commentId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> ChangeVoteComment([FromRoute] Guid commentId)
    {
        await Sender.Send(new ChangeVoteCommentCommand
        {
            CommentId = commentId,
            UserId = UserId
        });

        return NoContent();
    }
}
