using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeCommentController : ControllerBase
    {


        private readonly ILikeComment _likeCommentService;
        public LikeCommentController(ILikeComment likeCommentService)
        {
            _likeCommentService = likeCommentService;
        }

        [HttpPost, Route("UpvoteCmt")]
        public async Task<IActionResult> Upvote(LikeComment
            likecmt)
        {
            var result = await _likeCommentService.AddUpvote(likecmt);
            return Ok(result);
        }

        [HttpPost, Route("DownvoteCmt")]
        public async Task<IActionResult> DownVote(LikeComment likecmt)
        {
            var result = await _likeCommentService.AddDownVote(likecmt);
            return Ok(result);
        }

    }
}
