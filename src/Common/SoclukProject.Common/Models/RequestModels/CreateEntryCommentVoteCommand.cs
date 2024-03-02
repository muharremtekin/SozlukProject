using MediatR;
using SoclukProject.Common.ViewModels;

namespace SoclukProject.Common.Models.RequestModels;
public class CreateEntryCommentVoteCommand : IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType VoteType { get; set; }
    public CreateEntryCommentVoteCommand()
    {

    }
    public CreateEntryCommentVoteCommand(Guid entryCommentId, Guid CreatedBy, VoteType voteType)
    {
        EntryCommentId = entryCommentId;
        CreatedBy = CreatedBy;
        VoteType = voteType;
    }
}

