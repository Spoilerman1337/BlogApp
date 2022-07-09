using BlogApp.Auth.Application.Common.Exceptions;
using BlogApp.Auth.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Auth.Application.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly UserManager<AppUser> _userManager;

    public UpdateUserCommandHandler(UserManager<AppUser> userManager) => _userManager = userManager;

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _userManager.Users.Where(x => x.Id == request.Id).SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(AppUser), request.Id);
        }

        entity.UserName = request.UserName;
        entity.Email = request.Email;
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Patronymic = request.Patronymic;

        await _userManager.UpdateAsync(entity);

        return Unit.Value;
    }
}
