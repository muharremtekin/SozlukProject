using MediatR;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.SearchEntry;
public class SearchEntryQuery : IRequest<List<SearchEntryViewmodel>>
{
    public SearchEntryQuery(string searchText)
    {
        SearchText = searchText;
    }

    public string SearchText { get; set; }

}

