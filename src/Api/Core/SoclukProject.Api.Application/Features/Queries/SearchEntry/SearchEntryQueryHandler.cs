using MediatR;
using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.SearchEntry;
public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewmodel>>
{
    private readonly IEntryRepository _entryRepository;

    public SearchEntryQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<List<SearchEntryViewmodel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
    {
        var result = _entryRepository
            .Get(e => EF.Functions.Like(e.Subject, $"{request.SearchText}%"))
            .Select(e => new SearchEntryViewmodel()
            {
                Id = e.Id,
                Subject = e.Subject,

            });

        return await result.ToListAsync();
    }
}

