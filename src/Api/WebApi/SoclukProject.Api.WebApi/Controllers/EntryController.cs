using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoclukProject.Api.Application.Features.Commands.Entry.CreateFav;
using SoclukProject.Api.Application.Features.Commands.Entry.DeleteFav;
using SoclukProject.Api.Application.Features.Commands.Entry.DeleteVote;
using SoclukProject.Api.Application.Features.Queries.GetEntries;
using SoclukProject.Api.Application.Features.Queries.GetEntryComments;
using SoclukProject.Api.Application.Features.Queries.GetEntryDetail;
using SoclukProject.Api.Application.Features.Queries.SearchEntry;
using SoclukProject.Common.Models;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.WebApi.Controllers;

[Route("api/entries")]
[ApiController]
public class EntryController : BaseController
{
    private readonly IMediator _mediator;

    public EntryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
    {
        var entries = await _mediator.Send(query);
        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneEntry([FromQuery] Guid id)
    {
        var result = await _mediator.Send(new GetEntryDetailQuery(id, UserId));

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}/comments")]
    public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
    {
        var result = await _mediator.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));

        return Ok(result);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Route("{entryId}/vote")]
    public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
    {
        var result = await _mediator.Send(new CreateEntryVoteCommand(entryId, voteType, UserId.Value));

        return Ok(result);
    }

    [HttpDelete]
    [Route("{entryId}/vote")]
    public async Task<IActionResult> DeleteEntryVote([FromRoute] Guid entryId)
    {
        await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

        return Ok();
    }

    [HttpPost]
    [Route("{entryId}/favorite")]
    public async Task<IActionResult> CreateEntryFav(Guid entryId)
    {
        var result = await _mediator.Send(new CreateEntryFavCommand(entryId, UserId));

        return Ok(result);
    }
    [HttpDelete]
    [Route("{entryId}/favorite")]
    public async Task<IActionResult> DeleteEntryFav(Guid entryId)
    {
        var result = await _mediator.Send(new DeleteEntryFavCommand(entryId, UserId.Value));

        return Ok(result);
    }
}

