using MediatR;
using SoclukProject.Common;
using SoclukProject.Common.Events.EntryComment;
using SoclukProject.Common.Infrastructure;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.Application.Features.Commands.EntryComment.CreateVote;
public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
{
    public Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
            exchangeName: SozlukConstants.VoteExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.CreateEntryCommentVoteQueueName,
            obj: new CreateEntryCommentVoteEvent()
            {
                EntryCommentId = request.EntryCommentId,
                VoteType = request.VoteType,
                CreatedBy = request.CreatedBy
            });

        return Task.FromResult(true);
    }
}

