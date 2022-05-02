using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Auth.Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ApiControllerBase : Controller
{
    private ISender _sender = null;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
