using MediatR;
using SoclukProject.Common.Models.Pages;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.GetMainPageEntries;
public class GetMainPageEntriesQuery : BasePagedQuery, IRequest<PagedViewmodel<GetEntryDetailViewmodel>>
{
    public GetMainPageEntriesQuery(Guid userId, int page, int pageSize) : base(page, pageSize)
    {
        UserId = userId;
    }

    public Guid? UserId { get; set; }
}
