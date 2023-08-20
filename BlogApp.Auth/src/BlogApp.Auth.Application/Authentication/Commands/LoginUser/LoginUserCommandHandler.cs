using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Authentication.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, SignInResult>
{
    private protected SignInManager<UserEntity> _signInManager;

    public LoginUserCommandHandler(SignInManager<UserEntity> signInManager) => _signInManager = signInManager;

    public async Task<SignInResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
    }
}
