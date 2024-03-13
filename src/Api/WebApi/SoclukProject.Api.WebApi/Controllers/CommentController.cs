using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoclukProject.Api.Application.Features.Commands.EntryComment.CreateFav;
using SoclukProject.Api.Application.Features.Commands.EntryComment.DeleteFav;
using SoclukProject.Api.Application.Features.Commands.EntryComment.DeleteVote;
using SoclukProject.Common.Models;
using SoclukProject.Common.Models.RequestModels;

namespace SoclukProject.Api.WebApi.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        [Route("{commentId}/vote")]
        public async Task<IActionResult> CreateEntryVote(Guid commentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _mediator
                .Send(new CreateEntryCommentVoteCommand(commentId, UserId.Value, voteType));

            return Ok(result);
        }

        [HttpDelete]
        [Route("{commentId}/vote")]
        public async Task<IActionResult> DeleteEntryVote([FromRoute] Guid commentId)
        {
            await _mediator.Send(new DeleteEntryCommentVoteCommand(commentId, UserId.Value));

            return Ok();
        }

        [HttpPost]
        [Route("{commentId}/favorite")]
        public async Task<IActionResult> CreateEntryCommentFav(Guid commentId)
        {
            var result = await _mediator.Send(new CreateEntryCommentFavCommand(commentId, UserId.Value));

            return Ok(result);
        }

        [HttpDelete]
        [Route("{commentId}/favorite")]
        public async Task<IActionResult> DeleteEntryCommentFav(Guid commentId)
        {
            var result = await _mediator.Send(new DeleteEntryCommentFavCommand(commentId, UserId.Value));

            return Ok(result);
        }
    }
}
