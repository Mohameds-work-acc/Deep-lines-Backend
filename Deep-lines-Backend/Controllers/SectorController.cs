using Deep_lines_Backend.BLL.DTOs.SectorEntity;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Deep_lines_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService sectorService;

        public SectorController(ISectorService sectorService)
        {
            this.sectorService = sectorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sectors = await sectorService.GetAll();
            return Ok(sectors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sector = await sectorService.GetById(id);
            if (sector == null) return NotFound();
            return Ok(sector);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddSectorDTO dto)
        {
            if (dto == null) return BadRequest();
            await sectorService.AddSector(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddSectorDTO dto)
        {
            if (dto == null) return BadRequest();
            var updated = await sectorService.UpdateSector(dto, id);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await sectorService.DeleteSector(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
