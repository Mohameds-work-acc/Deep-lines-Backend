using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deep_lines_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await blogService.GetAll();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await blogService.GetById(id);
            if (blog == null) return NotFound();
            return Ok(blog);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddBlogDTO blogDto)
        {
            if (blogDto == null) return BadRequest();

            await blogService.AddBlog(blogDto);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddBlogDTO blogDto)
        {
            if (blogDto == null) return BadRequest();

            var updated = await blogService.UpdateBlog(blogDto, id);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await blogService.DeleteBlog(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
