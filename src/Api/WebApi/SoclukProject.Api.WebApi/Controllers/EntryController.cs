using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.WebApi.Controllers;

[Route("api/entries")]
[ApiController]
public class EntryController : ControllerBase
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
}

