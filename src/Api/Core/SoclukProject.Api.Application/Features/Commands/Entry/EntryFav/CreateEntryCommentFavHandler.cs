using MediatR;
using SoclukProject.Common;
using SoclukProject.Common.Events.EntryComment;
using SoclukProject.Common.Infrastructure;

namespace SoclukProject.Api.Application.Features.Commands.Entry.EntryFav;

public class CreateEntryCommentFavHandler : IRequestHandler<CreateEntryCommentFav, bool>
{
    public async Task<bool> Handle(CreateEntryCommentFav request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryCommentFavQueueName,
            obj: new CreateEntryCommentFavEvent()
            {
                CreatedById = request.UserId,
                EntryCommentId = request.EntryCommentId,
            });
        return await Task.FromResult(true);
    }
}

