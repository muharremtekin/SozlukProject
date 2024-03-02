using MediatR;

namespace SoclukProject.Common.Models.RequestModels;
public class CreateEntryCommentCommand : IRequest<Guid>
{
    public CreateEntryCommentCommand()
    {
    }

    public CreateEntryCommentCommand(Guid entryId, Guid createdById, string content)
    {
        EntryId = entryId;
        CreatedById = createdById;
        Content = content;
    }

    public Guid EntryId { get; set; }
    public Guid CreatedById { get; set; }
    public string Content { get; set; }
}

