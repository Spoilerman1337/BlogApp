using AutoMapper;
using BlogApp.Auth.Application.Users.Commands.CreateUser;
using BlogApp.Auth.Application.Users.Commands.CreateUser.Models;
using BlogApp.Auth.Application.Users.Commands.DeleteUser;
using BlogApp.Auth.Application.Users.Commands.SetUserRole;
using BlogApp.Auth.Application.Users.Commands.SetUserRole.Models;
using BlogApp.Auth.Application.Users.Commands.UpdateUser;
using BlogApp.Auth.Application.Users.Commands.UpdateUser.Models;
using BlogApp.Auth.Application.Users.Queries.GetUser;
using BlogApp.Auth.Application.Users.Queries.GetUser.Models;
using BlogApp.Auth.Application.Users.Queries.GetUsers;
using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Auth.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Produces("application/json")]
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
    [Authorize]
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
    /// GET /user
    /// </remarks>
    /// <returns>Returns GetUsersDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<GetUsersDto>> GetAllUsers()
    {
        var vm = await Sender.Send(new GetUsersQuery());
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
