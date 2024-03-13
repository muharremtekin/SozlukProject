using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoclukProject.Api.Application.Features.Commands.User.ConfirmEmail;
using SoclukProject.Api.Application.Features.Queries.GetUserDetail;
using SoclukProject.Api.Application.Features.Queries.GetUserEntries;
using SoclukProject.Common.Events.User;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.WebApi.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : BaseController
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

    [HttpGet]
    [Route("{userId}/entries")]
    public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
    {
        if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
            userId = UserId.Value;

        var result = await _mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

        return Ok(result);
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetUser([FromQuery] Guid userId)
    {
        var result = await _mediator.Send(new GetUserDetailQuery(userId));
        return Ok(result);
    }

    //[HttpGet]
    //[Route("username/{userName}")]
    //public async Task<IActionResult> GetByUserName(string userName)
    //{
    //    var user = await _mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

    //    return Ok(user);
    //}

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

