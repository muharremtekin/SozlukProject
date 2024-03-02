using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.WebApi.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
