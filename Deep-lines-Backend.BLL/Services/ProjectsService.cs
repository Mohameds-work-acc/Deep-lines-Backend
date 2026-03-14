using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.ProjectsEntity;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Deep_lines_Backend.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IGenericRepo<Projects> repo;
        private readonly IEmployeeService employeeService;
        private readonly ISectorService sectorService;
        private readonly IGenericRepo<Sector> sectorRepo;
        private readonly IMapper mapper;
        private readonly CloudinaryService cloudinaryService;

        public ProjectsService(IGenericRepo<Projects> repo, IEmployeeService employeeService, IGenericRepo<Sector> sectorRepo, IMapper mapper, CloudinaryService cloudinaryService, ISectorService sectorService)
        {
            this.repo = repo;
            this.employeeService = employeeService;
            this.sectorRepo = sectorRepo;
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
            this.sectorService = sectorService;
        }

        public async Task AddProject(AddProjectsDTO projectDTO)
        {
            var mapped = mapper.Map<Projects>(projectDTO);
            if (projectDTO.User_Id.HasValue)
            {
                mapped.addedBy = projectDTO.User_Id;
            }
            if (projectDTO.Sector_Id.HasValue)
            {
                mapped.sector = await sectorRepo.GetByIdAsync(projectDTO.Sector_Id.Value);
            }
            if (projectDTO.image != null)
            {
                var uploadResult = await cloudinaryService.UploadImageAsync(projectDTO.image, "Projects");
                if (uploadResult != null)
                {
                    mapped.ImagePublicId = uploadResult.PublicId;
                    mapped.ImageUrl = uploadResult.Url;
                }
            }

            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project = await repo.GetByIdAsync(id);
            if (project == null) return false;
            if (!string.IsNullOrEmpty(project.ImagePublicId))
            {
                await cloudinaryService.DeleteImageAsync(project.ImagePublicId);
            }
            await repo.DeleteAsync(project);
            return true;
        }

        public async Task<List<getProjectsDTO>> GetAll()
        {
            var projects = await repo.GetAllAsync();
            var mappedProjects = new List<getProjectsDTO>();

            var relatedEmployees = projects.Where(p => p.addedBy.HasValue).Select(p => p.addedBy.Value).Distinct().ToList();
            var allEmployees = await employeeService.GetByIds(relatedEmployees);
            var employeeDictionary = allEmployees.ToDictionary(e => e.Id);
            var sectorIds = projects.Where(p=>p.SectorId != null).Select(p=>p.SectorId).Distinct().ToList();
            var allSectors = await sectorService.GetByIdsAsync(sectorIds);
            var sectorDictionary = allSectors.ToDictionary(s => s.Id);
            foreach (var project in projects)
            {
                var dto = mapper.Map<getProjectsDTO>(project);
                if (project.addedBy.HasValue)
                {
                    if (employeeDictionary.TryGetValue(project.addedBy.Value, out var employee))
                    {
                        var mappedEmployee = mapper.Map<getBlogUserDTO>(employee);
                        dto.author = mappedEmployee;
                    }
                    if (project.sector != null)
                    {
                        if (sectorDictionary.TryGetValue(project.sector.Id, out var sector))
                        {
                           var sectorDTO = new getSectorWithProjectDTO
                            {
                                Id = sector.Id,
                                title = sector.title
                           };
                            var sectorTitle = sector.title;
                            dto.sector = sectorDTO;
                        }
                    }
                }
                mappedProjects.Add(dto);
            }

            return mappedProjects;
        }

        public async Task<getProjectsDTO> GetById(int id)
        {
            var project = await repo.GetByIdAsync(id);
            if (project == null) return null;
            var dto = mapper.Map<getProjectsDTO>(project);
            if (project.addedBy.HasValue)
            {
                var employee = await employeeService.GetById(project.addedBy.Value);
                if (employee != null)
                {
                    dto.author = mapper.Map<getBlogUserDTO>(employee);
                }
            }
            return dto;
        }

        public async Task<bool> UpdateProject(AddProjectsDTO projectDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(projectDTO, recorded);
            if (projectDTO.User_Id.HasValue)
            {
                recorded.updatedBy = projectDTO.User_Id;
            }
            if (projectDTO.Sector_Id.HasValue)
            {
                recorded.sector = await sectorRepo.GetByIdAsync(projectDTO.Sector_Id.Value);
            }
            if (projectDTO.image != null)
            {
                var uploadResult = await cloudinaryService.UpdateImageAsync(recorded.ImagePublicId, projectDTO.image, "Projects");
                if (uploadResult != null)
                {
                    recorded.ImagePublicId = uploadResult.PublicId;
                    recorded.ImageUrl = uploadResult.Url;
                }
            }

            repo.UpdateAsync(recorded);
            return true;
        }
    }
}
