using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.SectorEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class SectorService : ISectorService
    {
        private readonly IGenericRepo<Sector> repo;
        private readonly IGenericRepo<Employee> userRepo;
        private readonly IMapper mapper;

        public SectorService(IGenericRepo<Sector> repo, IGenericRepo<Employee> userRepo, IMapper mapper)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.mapper = mapper;
        }

        public async Task AddSector(AddSectorDTO sectorDTO)
        {
            var mapped = mapper.Map<Sector>(sectorDTO);
            if (sectorDTO.User_Id.HasValue)
            {
                mapped.addedBy = sectorDTO.User_Id;
            }
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteSector(int id)
        {
            var sector = await repo.GetByIdAsync(id);
            if (sector == null) return false;
            await repo.DeleteAsync(sector);
            return true;
        }

        public Task<List<Sector>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<Sector> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateSector(AddSectorDTO sectorDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(sectorDTO, recorded);
            if (sectorDTO.User_Id.HasValue)
            {
                recorded.updatedBy = sectorDTO.User_Id;
            }
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
