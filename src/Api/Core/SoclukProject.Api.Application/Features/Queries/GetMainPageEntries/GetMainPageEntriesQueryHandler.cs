using MediatR;
using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Infrastructure.Extensions;
using SoclukProject.Common.Models.Pages;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewmodel<GetEntryDetailViewmodel>>
{
    public IEntryRepository _entryRepository { get; set; }
    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<PagedViewmodel<GetEntryDetailViewmodel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query
            .Include(e => e.EntryFavorites)
            .Include(e => e.CreatedBy)
            .Include(e => e.EntryVotes);

        var list = query.Select(e => new GetEntryDetailViewmodel()
        {
            Id = e.Id,
            Subject = e.Subject,
            Content = e.Content,
            IsFavorited = request.UserId.HasValue && e.EntryFavorites.Any(j => j.CreatedById == request.UserId),
            FavoritedCount = e.EntryFavorites.Count,
            CreatedDate = e.CreateDate,
            CreatedByUserName = e.CreatedBy.UserName,
            VoteType =
                request.UserId.HasValue && e.EntryVotes.Any(j => j.CreatedById == request.UserId)
                ? e.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                : Common.Models.VoteType.None
        });

        var entries = await list.ToPaged(request.Page, request.PageSize);

        return entries;
    }
}