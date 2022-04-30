﻿using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Auth.Application.Users.Commands.LoginUser;

public class LoginUserCommand : IRequest<SignInResult>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
