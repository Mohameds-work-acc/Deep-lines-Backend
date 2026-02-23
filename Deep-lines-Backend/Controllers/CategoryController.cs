using Deep_lines_Backend.BLL.DTOs.CategoryEntity;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deep_lines_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await categoryService.GetAll();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddCategoryDTO dto)
        {
            if (dto == null) return BadRequest();
            await categoryService.AddCategory(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddCategoryDTO dto)
        {
            if (dto == null) return BadRequest();
            var updated = await categoryService.UpdateCategory(dto, id);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await categoryService.DeleteBlog(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
