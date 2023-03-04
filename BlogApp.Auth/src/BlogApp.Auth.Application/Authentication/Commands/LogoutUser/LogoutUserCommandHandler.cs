using BlogApp.Auth.Application.Authentication.Commands.LogoutUser;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Authentication.Commands.LoginUser;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
{
    private protected SignInManager<AppUser> _signInManager;

    public LogoutUserCommandHandler(SignInManager<AppUser> signInManager) => _signInManager = signInManager;

    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}
