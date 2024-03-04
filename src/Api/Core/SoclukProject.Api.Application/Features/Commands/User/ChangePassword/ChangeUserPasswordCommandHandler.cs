using MediatR;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Events.User;
using SoclukProject.Common.Infrastructure;
using SoclukProject.Common.Infrastructure.Exceptions;

namespace SoclukProject.Api.Application.Features.Commands.User.ChangePassword;
public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
            throw new ArgumentNullException(nameof(request.UserId));

        var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);

        if (dbUser is null)
            throw new DatabaseValidationException("User not found!");

        var encOldPass = PasswordEncryptor.Encrypt(request.OldPassword);

        if (encOldPass != dbUser.Password)
            throw new DatabaseValidationException("Old password is wrong!");

        dbUser.Password = PasswordEncryptor.Encrypt(request.NewPassword);

        await _userRepository.UpdateAsync(dbUser);

        return true;
    }
}

