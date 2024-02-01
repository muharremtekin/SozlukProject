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
    public async Task<IActionResult> Login([FromRoute] LoginUserCommand loginUserCommand)
    {
        var res = await _mediator.Send(loginUserCommand);
        return Ok(res);
    }
}

