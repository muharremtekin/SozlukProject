using MediatR;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Infrastructure.Exceptions;

namespace SoclukProject.Api.Application.Features.Commands.User.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailConfirmationRepository _emailConfirmationRepository;

    public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
    {
        _userRepository = userRepository;
        _emailConfirmationRepository = emailConfirmationRepository;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var confirmation = await _emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

        if (confirmation is null)
            throw new DatabaseValidationException("Confirmation not found!");

        var dbUser = await _userRepository.GetSingleAsync(u => u.EmailAddress == confirmation.NewEmailAddress);

        if (dbUser is null)
            throw new DatabaseValidationException("User not found with this email!");

        if (dbUser.EmailConfirmed)
            throw new DatabaseValidationException("Email address is already confirmed!");

        dbUser.EmailConfirmed = true;
        await _userRepository.UpdateAsync(dbUser);

        return true;
    }
}