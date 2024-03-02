using MediatR;
using SoclukProject.Common;
using SoclukProject.Common.Events.Entry;
using SoclukProject.Common.Infrastructure;

namespace SoclukProject.Api.Application.Features.Commands.Entry.CreateFav;

public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryFavQueueName,
            obj: new CreateEntryFavEvent()
            {
                EntryId = request.EntryId.Value,
                CreatedBy = request.UserId.Value
            });

        return await Task.FromResult(true);
    }
}

