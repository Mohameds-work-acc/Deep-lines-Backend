using Deep_lines_Backend.BLL.DTOs.CommentEntity;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Deep_lines_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }


        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetByBlogId(int blogId)
        {
            var comments = await commentService.GetByBlogId(blogId);
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Create( AddCommentDTO dto)
        {
            if (dto == null) return BadRequest();
            await commentService.AddComment(dto);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await commentService.DeleteComment(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
