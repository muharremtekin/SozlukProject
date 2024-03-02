namespace SoclukProject.Api.Application.Features.Commands.Entry.DeleteFav;

public class DeleteEntryFavEvent
{
    public Guid EntryId { get; set; }
    public Guid CreatedBy { get; set; }
}

