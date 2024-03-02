using MediatR;
using SoclukProject.Common.ViewModels;

namespace SoclukProject.Common.Models.RequestModels;

public class CreateEntryVoteCommand : IRequest<bool>
{
    public Guid EntryId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedBy { get; set; }
}

