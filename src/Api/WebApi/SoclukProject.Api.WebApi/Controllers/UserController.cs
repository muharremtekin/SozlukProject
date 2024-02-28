using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.WebApi.Controllers;

[Route("api/[controller]")]
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
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var res = await _mediator.Send(loginUserCommand);
        return Ok(res);
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand createUserCommand)
    {
        var guid = await _mediator.Send(createUserCommand);
        return Ok(guid);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand)
    {
        var guid = await _mediator.Send(updateUserCommand);
        return Ok(guid);
    }
}

