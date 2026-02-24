using Deep_lines_Backend.BLL.DTOs.ProjectsEntity;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace Deep_lines_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            this.projectsService = projectsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await projectsService.GetAll();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await projectsService.GetById(id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddProjectsDTO dto)
        {
            if (dto == null) return BadRequest();
            await projectsService.AddProject(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AddProjectsDTO dto)
        {
            if (dto == null) return BadRequest();
            var updated = await projectsService.UpdateProject(dto, id);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await projectsService.DeleteProject(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
