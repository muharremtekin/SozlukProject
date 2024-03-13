using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Models.ViewModels;

namespace SoclukProject.Api.Application.Features.Queries.GetEntries;

public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
{
    private readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;

    public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if (request.TodaysEntries)
        {
            query = query
                .Where(e => e.CreateDate > DateTime.UtcNow.Date)
                .Where(e => e.CreateDate > DateTime.UtcNow.AddDays(1).Date);
        }

        query = query.Include(e => e.EntryComments)
            .OrderBy(e => e.EntryComments.Count)
            .Take(request.Count);

        return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}