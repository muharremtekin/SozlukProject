using SoclukProject.Common.ViewModels;

namespace SoclukProject.Common.Events.EntryComment;
public class CreateEntryCommentVoteEvent
{
    public Guid EntryCommentId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType VoteType { get; set; }
}

