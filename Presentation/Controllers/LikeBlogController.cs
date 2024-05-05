using Application.Interfaces;
using Domain.Entity;
using Infrastructures.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeBlogController : ControllerBase
    {
        private readonly ILike _likeBlogService;
        public LikeBlogController(ILike likeBlogService)
        {
            _likeBlogService = likeBlogService;
        }

        [HttpPost, Route("Upvote")]
        public async Task<IActionResult> Upvote(Like
            like)
        {
            var result = await _likeBlogService.AddUpvote(like);
            return Ok(result);
        }

        [HttpPost, Route("Downvote")]
        public async Task<IActionResult> DownVote(Like like)
        {
            var result = await _likeBlogService.AddDownVote(like);
            return Ok(result);
        }



    }
}
