using MediatR;
using SoclukProject.Common;
using SoclukProject.Common.Events.EntryComment;
using SoclukProject.Common.Infrastructure;

namespace SoclukProject.Api.Application.Features.Commands.EntryComment.CreateFav;
public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryCommentFavQueueName,
            obj: new CreateEntryCommentFavEvent()
            {
                EntryCommentId = request.EntryCommentId,
                CreatedBy = request.UserId
            });

        return await Task.FromResult(true);
    }
}
