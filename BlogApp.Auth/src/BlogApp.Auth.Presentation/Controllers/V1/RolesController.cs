﻿using BlogApp.Auth.Application.Roles.Commands.CreateRole;
using BlogApp.Auth.Application.Roles.Commands.CreateRole.Models;
using BlogApp.Auth.Application.Roles.Commands.DeleteRole;
using BlogApp.Auth.Application.Roles.Commands.UpdateRole;
using BlogApp.Auth.Application.Roles.Commands.UpdateRole.Models;
using BlogApp.Auth.Application.Roles.Queries.GetRole;
using BlogApp.Auth.Application.Roles.Queries.GetRole.Models;
using BlogApp.Auth.Application.Roles.Queries.GetRoles;
using BlogApp.Auth.Application.Roles.Queries.GetRoles.Models;
using BlogApp.Auth.Application.Roles.Queries.GetUsersRole;
using BlogApp.Auth.Application.Roles.Queries.GetUsersRole.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Auth.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize]
public class RolesController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public RolesController(IMapper mapper) => _mapper = mapper;

    /// <summary>Gets role by id</summary>
    /// <remarks>
    /// Sample request:
    /// GET /role/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a role</param>
    /// <returns>Returns GetRoleDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetRoleDto>> GetRole([FromRoute] Guid id)
    {
        var query = new GetRoleQuery
        {
            Id = id
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets all roles</summary>
    /// <remarks>
    /// Sample request:
    /// GET /role?page[eq]=2&amp;pageSize[eq]=2
    /// </remarks>
    /// <param name="queryParams">Dictionary containing filters. Available: bypassCache[eq]</param>
    /// <returns>Returns List of GetRolesDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<GetRolesDto>>> GetAllRoles([FromQuery] Dictionary<string, Dictionary<string, string>> queryParams)
    {
        var query = new GetRolesQuery()
        {
            Page = GetParam<int>(queryParams, "page", "eq"),
            PageAmount = GetParam<int>(queryParams, "pageSize", "eq")
        };
        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets users role</summary>
    /// <remarks>
    /// Sample request:
    /// GET /role/user/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <returns>Returns GetUsersRoleDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetUsersRoleDto>> GetUsersRole(Guid userId)
    {
        var query = new GetUsersRoleQuery
        {
            UserId = userId
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

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
        var command = _mapper.Map<UpdateRoleCommand>(dto);

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
