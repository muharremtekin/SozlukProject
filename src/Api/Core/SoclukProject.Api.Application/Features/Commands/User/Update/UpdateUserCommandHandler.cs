using AutoMapper;
using MediatR;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common;
using SoclukProject.Common.Events.User;
using SoclukProject.Common.Infrastructure;
using SoclukProject.Common.Infrastructure.Exceptions;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.Application.Features.Commands.User.Update;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);


        if (user is null)
            throw new DatabaseValidationException("User not found!");

        var userEmailAddress = user.EmailAddress;
        var emailChanged = string.CompareOrdinal(userEmailAddress, request.EmailAddress) != 0;

        _mapper.Map(request, user);

        var rows = await _userRepository.UpdateAsync(user);

        if (emailChanged && rows > 0)
        {
            var @event = new UserEmailChangeEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = user.EmailAddress,
            };
            QueueFactory.SendMessageToExchange(
                exchangeName: SozlukConstants.UserExchangeName,
                exchangeType: SozlukConstants.DefaultExchangeType,
                queueName: SozlukConstants.DefaultExchangeType,
                obj: @event);

            user.EmailConfirmed = false;
            await _userRepository.UpdateAsync(user);
        }

        return user.Id;
    }
}

