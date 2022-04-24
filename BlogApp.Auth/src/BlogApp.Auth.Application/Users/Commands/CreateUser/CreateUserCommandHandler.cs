using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
{
    private readonly UserManager<AppUser> _userManager;

    public CreateUserCommandHandler(UserManager<AppUser> userManager) => _userManager = userManager;

    public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return result;
    }
}
