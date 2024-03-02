using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoclukProject.Api.Application.Features.Commands.User.ConfirmEmail;
using SoclukProject.Common.Events.User;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.WebApi.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var res = await _mediator.Send(command);
        return Ok(res);
    }
    [HttpPost]
    [Route("confirm")]
    public async Task<IActionResult> ConfirmEmail([FromRoute] Guid id)
    {
        var command = new ConfirmEmailCommand() { ConfirmationId = id };

        var res = await _mediator.Send(command);
        return Ok(res);
    }
    [HttpPost]
    [Route("changePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
    {
        var res = await _mediator.Send(command);
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var guid = await _mediator.Send(command);
        return Ok(guid);
    }
}

