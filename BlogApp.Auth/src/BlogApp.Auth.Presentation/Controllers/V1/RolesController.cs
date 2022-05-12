using AutoMapper;
using BlogApp.Auth.Application.Roles.Commands.CreateRole;
using BlogApp.Auth.Application.Roles.Commands.CreateRole.Models;
using BlogApp.Auth.Application.Roles.Commands.DeleteRole;
using BlogApp.Auth.Application.Roles.Commands.UpdateRole.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Auth.Presentation.Controllers.V1;

[ApiVersion("1.0")]
public class RolesController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public RolesController(IMapper mapper) => _mapper = mapper;

    /// <summary>Creates role</summary>
    /// <remarks>
    /// Sample request:
    /// POST /role
    /// {
    ///     name: "string"
    /// }
    /// </remarks>
    /// <param name="dto">CreateRoleDto object</param>
    /// <returns>Returns IdentityResult object</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IdentityResult>> CreateRole([FromBody] CreateRoleDto dto)
    {
        var command = _mapper.Map<CreateRoleCommand>(dto);

        return await Sender.Send(command);
    }

    /// <summary>Updates role</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /role/b5c0a7ae-762d-445d-be15-b59232b19383
    /// {
    ///     name: "string"
    /// }
    /// </remarks>
    /// <param name="id">GUID ID of a user</param>
    /// <param name="dto">UpdateRoleDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateRole([FromRoute] Guid id, [FromBody] UpdateRoleDto dto)
    {
        dto.Id = id;
        var command = _mapper.Map<CreateRoleCommand>(dto);

        await Sender.Send(command);
        return NoContent();
    }

    /// <summary>Deletes role</summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /role/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a role</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteRole([FromRoute] Guid id)
    {
        await Sender.Send(new DeleteRoleCommand
        {
            Id = id,
        });

        return NoContent();
    }
}
