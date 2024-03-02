using MediatR;
using SoclukProject.Common;
using SoclukProject.Common.Events.Entry;
using SoclukProject.Common.Infrastructure;

namespace SoclukProject.Api.Application.Features.Commands.Entry.DeleteVote;
public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
{
    public Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
            exchangeName: SozlukConstants.VoteExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.DeleteEntryVoteQueueName,
            obj: new DeleteEntryVoteEvent()
            {
                EntryId = request.EntryId,
                CreatedBy = request.UserId
            });

        return Task.FromResult(true);
    }
}

