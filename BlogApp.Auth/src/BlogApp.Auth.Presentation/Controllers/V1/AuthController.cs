using AutoMapper;
using BlogApp.Auth.Application.Users.Commands.LoginUser;
using BlogApp.Auth.Application.Users.Commands.LoginUser.Models;
using BlogApp.Auth.Application.Users.Queries.GetUser;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Auth.Presentation.Controllers.V1;

[ApiVersion("1.0")]
public class AuthController : ApiControllerBase
{
    private readonly IMapper _mapper;

    public AuthController(IMapper mapper) => _mapper = mapper;

    /// <summary>Login user</summary>
    /// <remarks>
    /// Sample request:
    /// GET /auth
    /// {
    ///     returnUrl: "string",
    ///     userId: "b5c0a7ae-762d-445d-be15-b59232b19383",
    /// }
    /// </remarks>
    /// <param name="returnUrl">Return URL string</param>
    /// <param name="userId">Return URL string</param>
    /// <returns>Returns View</returns>
    /// <response code="200">If valid</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Login([FromQuery] string returnUrl, [FromQuery] Guid userId)
    {
        var viewModel = new LoginUserDto
        {
            ReturnUrl = returnUrl,
            Id = userId
        };
        return View(viewModel);
    }

    /// <summary>User login redirect</summary>
    /// <remarks>
    /// Sample request:
    /// POST /auth
    /// {
    ///     userName: "string",
    ///     password: "string",
    ///     returnUrl: "string",
    /// }
    /// </remarks>
    /// <param name="dto">LoginUserDto object</param>
    /// <param name="userId">GUID user ID</param>
    /// <returns>Returns View</returns>
    /// <response code="200">If valid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        var vm = _mapper.Map<LoginUserCommand>(dto);

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var user = await Sender.Send(new GetUserQuery() { Id = dto.Id });

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(vm);
        }

        var result = await Sender.Send(vm);

        if (result.Succeeded)
        {
            return Redirect(dto.ReturnUrl);
        }
        ModelState.AddModelError(string.Empty, "Login error");

        return View(vm);
    }
}
