using BlogApp.Auth.Application.Users.Commands.LogoutUser;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Users.Commands.LoginUser;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
{
    private protected SignInManager<AppUser> _signInManager;

    public LogoutUserCommandHandler(SignInManager<AppUser> signInManager) => _signInManager = signInManager;

    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();

        return Unit.Value;
    }
}
