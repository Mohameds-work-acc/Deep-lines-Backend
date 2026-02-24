using Deep_lines_Backend.BLL.DTOs.OrderEntity;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Deep_lines_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await orderService.GetAll();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await orderService.GetById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddOrderDTO dto)
        {
            if (dto == null) return BadRequest();
            await orderService.AddOrder(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddOrderDTO dto)
        {
            if (dto == null) return BadRequest();
            var updated = await orderService.UpdateOrder(dto, id);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await orderService.DeleteOrder(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
