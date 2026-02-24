using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.ProjectsEntity;
using Deep_lines_Backend.BLL.Interfaces;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IGenericRepo<Projects> repo;
        private readonly IGenericRepo<User> userRepo;
        private readonly IGenericRepo<Sector> sectorRepo;
        private readonly IMapper mapper;

        public ProjectsService(IGenericRepo<Projects> repo, IGenericRepo<User> userRepo, IGenericRepo<Sector> sectorRepo, IMapper mapper)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.sectorRepo = sectorRepo;
            this.mapper = mapper;
        }

        public async Task AddProject(AddProjectsDTO projectDTO)
        {
            var mapped = mapper.Map<Projects>(projectDTO);
            if (projectDTO.User_Id.HasValue)
            {
                mapped.user = await userRepo.GetByIdAsync(projectDTO.User_Id.Value);
            }
            if (projectDTO.Sector_Id.HasValue)
            {
                mapped.sector = await sectorRepo.GetByIdAsync(projectDTO.Sector_Id.Value);
            }
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteProject(int id)
        {
            var project = await repo.GetByIdAsync(id);
            if (project == null) return false;
            await repo.DeleteAsync(project);
            return true;
        }

        public Task<List<Projects>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<Projects> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateProject(AddProjectsDTO projectDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(projectDTO, recorded);
            if (projectDTO.User_Id.HasValue)
            {
                recorded.user = await userRepo.GetByIdAsync(projectDTO.User_Id.Value);
            }
            if (projectDTO.Sector_Id.HasValue)
            {
                recorded.sector = await sectorRepo.GetByIdAsync(projectDTO.Sector_Id.Value);
            }
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
