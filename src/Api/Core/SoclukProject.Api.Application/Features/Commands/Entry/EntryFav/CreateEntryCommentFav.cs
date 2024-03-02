using MediatR;

namespace SoclukProject.Api.Application.Features.Commands.Entry.EntryFav;

public class CreateEntryCommentFav : IRequest<bool>
{
    public CreateEntryCommentFav(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }

    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }
}

