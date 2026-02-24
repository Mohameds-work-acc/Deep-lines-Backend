using Deep_lines_Backend.BLL.DTOs.ReviewEntity;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Deep_lines_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await reviewService.GetAll();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await reviewService.GetById(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddReviewDTO dto)
        {
            if (dto == null) return BadRequest();
            await reviewService.AddReview(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddReviewDTO dto)
        {
            if (dto == null) return BadRequest();
            var updated = await reviewService.UpdateReview(dto, id);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await reviewService.DeleteReview(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
