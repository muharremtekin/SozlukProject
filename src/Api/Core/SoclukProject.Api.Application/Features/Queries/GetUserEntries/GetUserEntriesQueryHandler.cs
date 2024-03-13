using MediatR;
using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Infrastructure.Extensions;
using SoclukProject.Common.Models.Pages;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.GetUserEntries;
public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewmodel<GetUserEntriesDetailViewmodel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<PagedViewmodel<GetUserEntriesDetailViewmodel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
            query = query.Where(e => e.CreatedById == request.UserId);

        else if (!string.IsNullOrEmpty(request.UserName))
            query = query.Where(e => e.CreatedBy.UserName == request.UserName);

        else
            return null;

        query = query.Include(e => e.EntryFavorites)
                     .Include(e => e.CreatedBy);

        var list = query.Select(e => new GetUserEntriesDetailViewmodel()
        {
            Id = e.Id,
            Subject = e.Subject,
            Content = e.Content,
            CreatedByUserName = e.CreatedBy.UserName,
            CreatedDate = e.CreateDate,
            FavoritedCount = e.EntryFavorites.Count,
            IsFavorited = false
        });

        var entries = await list.ToPaged(request.Page, request.PageSize);

        return entries;
    }
}

