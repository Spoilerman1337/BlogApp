using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Authentication.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, SignInResult>
{
    private protected SignInManager<AppUser> _signInManager;

    public LoginUserCommandHandler(SignInManager<AppUser> signInManager) => _signInManager = signInManager;

    public async Task<SignInResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
    }
}
