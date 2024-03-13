using SoclukProject.Common.Models;

namespace SoclukProject.Common.Events.Entry;

public class CreateEntryVoteEvent
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
    public VoteType VoteType { get; set; }
}

