using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
}
