﻿using BlogApp.Auth.Application.Users.Commands.CreateUser;
using BlogApp.Auth.Application.Users.Commands.CreateUser.Models;
using BlogApp.Auth.Application.Users.Commands.DeleteUser;
using BlogApp.Auth.Application.Users.Commands.SetUserRole;
using BlogApp.Auth.Application.Users.Commands.SetUserRole.Models;
using BlogApp.Auth.Application.Users.Commands.UpdateUser;
using BlogApp.Auth.Application.Users.Commands.UpdateUser.Models;
using BlogApp.Auth.Application.Users.Queries.GetUser;
using BlogApp.Auth.Application.Users.Queries.GetUser.Models;
using BlogApp.Auth.Application.Users.Queries.GetUserByName;
using BlogApp.Auth.Application.Users.Queries.GetUserByName.Models;
using BlogApp.Auth.Application.Users.Queries.GetUsers;
using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using BlogApp.Auth.Application.Users.Queries.GetUsersByRole;
using BlogApp.Auth.Application.Users.Queries.GetUsersByRole.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Auth.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
[Authorize]
public class UserController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper) => _mapper = mapper;

    /// <summary>Gets user by id</summary>
    /// <remarks>
    /// Sample request:
    /// GET /user/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a user</param>
    /// <returns>Returns GetUserDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetUserDto>> GetUser([FromRoute] Guid id)
    {
        var query = new GetUserQuery
        {
            Id = id
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets all users</summary>
    /// <remarks>
    /// Sample request:
    /// GET /user?page[eq]=2&amp;pageSize[eq]=2
    /// </remarks>
    /// <param name="queryParams">Dictionary containing filters. Available: bypassCache[eq]</param>
    /// <returns>Returns List of GetUsersDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<GetUsersDto>>> GetAllUsers([FromQuery] Dictionary<string, Dictionary<string, string>> queryParams)
    {
        var query = new GetUsersQuery()
        {
            Page = GetParam<int>(queryParams, "page", "eq"),
            PageAmount = GetParam<int>(queryParams, "pageSize", "eq")
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets all users in role</summary>
    /// <remarks>
    /// Sample request:
    /// GET /user/role/b5c0a7ae-762d-445d-be15-b59232b19383?page=2&amp;pageAmount=2
    /// </remarks>
    /// <param name="roleId">GUID ID of a role</param>
    /// <param name="queryParams">Dictionary containing filters. Available: bypassCache[eq]</param>
    /// <returns>Returns List of GetUsersByRoleDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("role/{roleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<List<GetUsersByRoleDto>>> GetAllUsersFromRole([FromRoute] Guid roleId, [FromQuery] Dictionary<string, Dictionary<string, string>> queryParams)
    {
        var query = new GetUsersByRoleQuery
        {
            RoleId = roleId,
            Page = GetParam<int>(queryParams, "page", "eq"),
            PageAmount = GetParam<int>(queryParams, "pageSize", "eq")
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Gets user with provided username</summary>
    /// <remarks>
    /// Sample request:
    /// GET /user/name/string
    /// </remarks>
    /// <param name="userName"></param>
    /// <returns>Returns GetUserByNameDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet("name/{userName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetUserByNameDto>> GetUserByName([FromRoute] string userName)
    {
        var query = new GetUserByNameQuery
        {
            UserName = userName
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    /// <summary>Creates user</summary>
    /// <remarks>
    /// Sample request:
    /// POST /user
    /// {
    ///     userName: "string",
    ///     password: "string",
    ///     email: "string",
    ///     firstName: "string",
    ///     lastName: "string",
    ///     patronymic: "string",
    /// }
    /// </remarks>
    /// <param name="dto">CreateUserDto object</param>
    /// <returns>Returns IdentityResult object</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IdentityResult>> CreateUser([FromBody] CreateUserDto dto)
    {
        var command = _mapper.Map<CreateUserCommand>(dto);

        return await Sender.Send(command);
    }

    /// <summary>Updates user</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /user/b5c0a7ae-762d-445d-be15-b59232b19383
    /// {
    ///     userName: "string",
    ///     password: "string",
    ///     email: "string",
    ///     firstName: "string",
    ///     lastName: "string",
    ///     patronymic: "string",
    /// }
    /// </remarks>
    /// <param name="id">GUID ID of a user</param>
    /// <param name="dto">UpdateUserDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto dto)
    {
        dto.Id = id;
        var command = _mapper.Map<UpdateUserCommand>(dto);

        await Sender.Send(command);
        return NoContent();
    }

    /// <summary>Deletes user</summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /user/b5c0a7ae-762d-445d-be15-b59232b19383
    /// </remarks>
    /// <param name="id">GUID ID of a user</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid id)
    {
        await Sender.Send(new DeleteUserCommand
        {
            Id = id,
        });

        return NoContent();
    }

    /// <summary>Changes user's role</summary>
    /// <remarks>
    /// Sample request:
    /// PUT /user
    /// {
    ///     userId: "b5c0a7ae-762d-445d-be15-b59232b19383",
    ///     roleId: "b5c0a7ae-762d-445d-be15-b59232b19383"
    /// }
    /// </remarks>
    /// <param name="dto">SetUserRoleDto object</param>
    /// <response code="204">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> SetUserRole([FromBody] SetUserRoleDto dto)
    {
        await Sender.Send(new SetUserRoleCommand
        {
            RoleId = dto.RoleId,
            UserId = dto.UserId
        });

        return NoContent();
    }
}
