using AutoMapper;
using BlogApp.Auth.Application.Users.Commands.CreateUser;
using BlogApp.Auth.Application.Users.Commands.CreateUser.Models;
using BlogApp.Auth.Application.Users.Commands.DeleteUser;
using BlogApp.Auth.Application.Users.Commands.UpdateUser;
using BlogApp.Auth.Application.Users.Commands.UpdateUser.Models;
using BlogApp.Auth.Application.Users.Queries.GetUser;
using BlogApp.Auth.Application.Users.Queries.GetUser.Models;
using BlogApp.Auth.Application.Users.Queries.GetUsers;
using BlogApp.Auth.Application.Users.Queries.GetUsers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Auth.Presentation.Controllers.V1;

[ApiVersion("1.0")]
public class UserController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper) => _mapper = mapper;

    [HttpGet("id")]
    public async Task<ActionResult<GetUserDto>> GetUser([FromRoute] Guid id)
    {
        var query = new GetUserQuery
        {
            Id = id
        };

        var vm = await Sender.Send(query);
        return Ok(vm);
    }

    [HttpGet]
    public async Task<ActionResult<GetUsersDto>> GetAllUsers()
    {
        var vm = await Sender.Send(new GetUsersQuery());
        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<IdentityResult>> CreateUser([FromBody] CreateUserDto dto)
    {
        var command = _mapper.Map<CreateUserCommand>(dto);

        return await Sender.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IdentityResult>> UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserDto dto)
    {
        dto.Id = id;
        var command = _mapper.Map<UpdateUserCommand>(dto);

        await Sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] Guid id)
    {
        await Sender.Send(new DeleteUserCommand
        {
            Id = id,
        });

        return NoContent();
    }
}
