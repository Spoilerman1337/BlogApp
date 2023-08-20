using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _sender = null;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    internal Guid UserId => !(User.Identity?.IsAuthenticated ?? false)
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString());

    protected static T? GetParam<T>(Dictionary<string, Dictionary<string, string>> queryParams, string paramName, string filter) where T: IParsable<T>
    {
        if (!queryParams.TryGetValue(paramName, out var valueDict))
            return default(T);

        if (!valueDict.TryGetValue(filter, out var value))
            return default(T);

        return T.TryParse(value, null, out var result) ? result : default(T);
    }

    //At the time of commit IParsable<TSelf> is not implemented by string, yet stated as 'api-approved' at dotnet github
    //Will be removed when and if IParsable<TSelf> would be implemented by string.
    //ETA: .NET 8.0  (~nov 23)
    //To be updated
    protected static string? GetParamString(Dictionary<string, Dictionary<string, string>> queryParams, string paramName, string filter)
    {
        if (!queryParams.TryGetValue(paramName, out var valueDict))
            return null;

        return !valueDict.TryGetValue(filter, out var value) ? null : value;
    }
    
    //At the time of commit IParsable<TSelf> is not implemented by bool, yet stated as 'api-approved' at dotnet github
    //Will be removed when and if IParsable<TSelf> would be implemented by bool.
    //ETA: .NET 8.0  (~nov 23)
    //To be updated
    protected static bool? GetParamBoolean(Dictionary<string, Dictionary<string, string>> queryParams, string paramName, string filter)
    {
        if (!queryParams.TryGetValue(paramName, out var valueDict))
            return null;

        if (!valueDict.TryGetValue(filter, out var value))
            return null;

        return bool.TryParse(value, out var result) ? result : null;
    }
}
