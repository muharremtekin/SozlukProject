using MediatR;
using Microsoft.EntityFrameworkCore;
using SoclukProject.Api.Application.Interfaces.Repositories;
using SoclukProject.Common.Infrastructure.Extensions;
using SoclukProject.Common.Models.Pages;
using SoclukProject.Common.Models.ViewModels;


namespace SoclukProject.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewmodel<GetEntryCommentsViewmodel>>
{
    private readonly IEntryCommentRepository _entryCommentRepository;

    public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
    {
        _entryCommentRepository = entryCommentRepository;
    }

    public async Task<PagedViewmodel<GetEntryCommentsViewmodel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _entryCommentRepository.AsQueryable();

        query = query
            .Include(e => e.EntryCommentFavorites)
            .Include(e => e.CreatedBy)
            .Include(e => e.EntryCommentVotes)
            .Where(e => e.EntryId == request.EntryId);

        var list = query.Select(e => new GetEntryCommentsViewmodel()
        {
            Id = e.Id,
            Content = e.Content,
            IsFavorited = request.UserId.HasValue && e.EntryCommentFavorites.Any(j => j.CreatedById == request.UserId),
            FavoritedCount = e.EntryCommentFavorites.Count,
            CreatedDate = e.CreateDate,
            CreatedByUserName = e.CreatedBy.UserName,
            VoteType =
                request.UserId.HasValue && e.EntryCommentVotes.Any(j => j.CreatedById == request.UserId)
                ? e.EntryCommentVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                : Common.Models.VoteType.None
        });

        var entries = await list.ToPaged(request.Page, request.PageSize);

        return entries;
    }
}