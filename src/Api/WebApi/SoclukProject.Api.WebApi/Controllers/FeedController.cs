using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoclukProject.Api.Application.Features.Queries.GetMainPageEntries;

namespace SoclukProject.Api.WebApi.Controllers;
[Route("api/feed")]
[ApiController]
public class FeedController : BaseController
{
    private readonly IMediator _mediator;

    public FeedController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}/comments")]
    public async Task<IActionResult> Feed([FromQuery] GetMainPageEntriesQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

