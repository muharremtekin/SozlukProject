using AutoMapper;
using MediatR;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common;
using SoclukProject.Common.Events.User;
using SoclukProject.Common.Infrastructure;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userRepository.GetSingleAsync(e => e.EmailAddress == request.EmailAddress);

        if (existUser is not null)
            throw new DataMisalignedException("User already exist!");

        var newUser = _mapper.Map<Domain.Models.User>(request);

        var rows = await _userRepository.AddAsync(newUser);

        if (rows > 0)
        {
            var @event = new UserEmailChangeEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = newUser.EmailAddress,
            };
            QueueFactory.SendMessageToExchange(
                exchangeName: SozlukConstants.UserExchangeName,
                exchangeType: SozlukConstants.DefaultExchangeType,
                queueName: SozlukConstants.DefaultExchangeType,
                obj: @event);
        }

        return newUser.Id;
    }
}

