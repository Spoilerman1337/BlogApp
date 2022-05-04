using AutoMapper;
using BlogApp.Auth.Application.Users.Commands.LoginUser;
using BlogApp.Auth.Application.Users.Commands.LoginUser.Models;
using BlogApp.Auth.Application.Users.Commands.LogoutUser;
using BlogApp.Auth.Application.Users.Queries.GetUser;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Auth.Presentation.Controllers.V1;

[ApiVersion("1.0")]
[Route("[controller]/[action]")]
public class AuthenticationController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly IIdentityServerInteractionService _interactionService;

    public AuthenticationController(IMapper mapper) => _mapper = mapper;

    /// <summary>Login user</summary>
    /// <remarks>
    /// Sample request:
    /// GET /auth/login
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
    /// POST /auth/login
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

    /// <summary>User logout</summary>
    /// <remarks>
    /// Sample request:
    /// GET /auth/logout
    /// </remarks>
    /// <param name="dto">LoginUserDto object</param>
    /// <param name="userId">GUID user ID</param>
    /// <returns>Returns View</returns>
    /// <response code="200">If valid</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout([FromQuery] string logoutId)
    {
        await Sender.Send(new LogoutUserCommand());
        var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);
        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }

}
