﻿using MediatR;
using SoclukProject.Common;
using SoclukProject.Common.Events.EntryComment;
using SoclukProject.Common.Infrastructure;

namespace SoclukProject.Api.Application.Features.Commands.EntryComment.DeleteVote;
public class DeleteEntryCommentVoteCommand : IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }
    public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }
}

public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
            exchangeName: SozlukConstants.VoteExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.DeleteEntryCommentVoteQueueName,
            obj: new DeleteEntryCommentVoteEvent()
            {
                EntryCommentId = request.EntryCommentId,
                CreatedBy = request.UserId
            });

        return await Task.FromResult(true);
    }
}