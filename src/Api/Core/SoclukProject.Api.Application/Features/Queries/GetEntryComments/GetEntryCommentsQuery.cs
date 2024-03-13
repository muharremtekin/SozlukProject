using MediatR;
using SoclukProject.Common.Models.Pages;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.GetEntryComments;
public class GetEntryCommentsQuery : BasePagedQuery, IRequest<PagedViewmodel<GetEntryCommentsViewmodel>>
{
    public GetEntryCommentsQuery(Guid entryId, Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        EntryId = entryId;
        UserId = userId;
    }

    public Guid EntryId { get; set; }
    public Guid? UserId { get; set; }
}
