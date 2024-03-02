using MediatR;
using SoclukProject.Common;
using SoclukProject.Common.Events.Entry;
using SoclukProject.Common.Infrastructure;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.Application.Features.Commands.Entry.CreateVote;

public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.VoteExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryVoteQueueName,
            obj: new CreateEntryVoteEvent()
            {
                EntryId = request.EntryId,
                VoteType = request.VoteType,
                CreatedBy = request.CreatedBy,
            });

        return await Task.FromResult(true);
    }
}

