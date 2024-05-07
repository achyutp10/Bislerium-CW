using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IComment _commentService;

        public CommentController(IComment commentService)
        {
            _commentService = commentService;
        }

        [HttpPost, Route("AddComment")]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            // Add the comment to the database
            await _commentService.AddComment(comment);

            // Return a success response
            return Ok("Comment added successfully");
        }

        [HttpGet, Route("GetComment")]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var result = await _commentService.GetCommentById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet, Route("GetComments")]
        public async Task<IActionResult> GetAllComments()
        {
            var result = await _commentService.GetAllComment();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete, Route("DeleteComment")]
        public async Task<IActionResult> DeleteComment(Guid Id)
        {
            await _commentService.DeleteComment(Id);

            return Ok();
        }

        [HttpPut, Route("UpdateComment")]
        public async Task<IActionResult> UpdateComment(Comment cmt)
        {
            var result = await _commentService.UpdateComment(cmt);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
