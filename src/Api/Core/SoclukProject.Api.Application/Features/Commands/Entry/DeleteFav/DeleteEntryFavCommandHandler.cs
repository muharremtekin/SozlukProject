using MediatR;
using SoclukProject.Common;
using SoclukProject.Common.Infrastructure;

namespace SoclukProject.Api.Application.Features.Commands.Entry.DeleteFav;

public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
{
    public Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
            exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.DeleteEntryFavQueueName,
            obj: new DeleteEntryFavEvent()
            {
                EntryId = request.EntryId,
                CreatedBy = request.UserId
            });

        return Task.FromResult(true);
    }
}

