using MediatR;
using SoclukProject.Common.Models.Pages;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.GetUserEntries;
public class GetUserEntriesQuery : BasePagedQuery, IRequest<PagedViewmodel<GetUserEntriesDetailViewmodel>>
{
    public Guid? UserId { get; set; }
    public string UserName { get; set; }
    public GetUserEntriesQuery(Guid? userId, string userName = null, int page = 1, int pageSize = 10) : base(page, pageSize)
    {
        UserId = userId;
        UserName = userName;
    }

}

