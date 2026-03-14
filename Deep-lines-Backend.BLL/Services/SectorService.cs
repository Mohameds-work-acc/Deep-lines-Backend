using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.SectorEntity;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Deep_lines_Backend.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class SectorService : ISectorService
    {
        private readonly IGenericRepo<Sector> repo;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        private readonly CloudinaryService cloudinaryService;

        public SectorService(IGenericRepo<Sector> repo, IEmployeeService employeeService, IMapper mapper, CloudinaryService cloudinaryService)
        {
            this.repo = repo;
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddSector(AddSectorDTO sectorDTO)
        {
            var mapped = mapper.Map<Sector>(sectorDTO);
            if (sectorDTO.User_Id.HasValue)
            {
                mapped.addedBy = sectorDTO.User_Id;
            }

            if (sectorDTO.image != null)
            {
                var uploadResult = await cloudinaryService.UploadImageAsync(sectorDTO.image, "Sectors");
                if (uploadResult != null)
                {
                    mapped.ImagePublicId = uploadResult.PublicId;
                    mapped.ImageUrl = uploadResult.Url;
                }
            }

            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteSector(int id)
        {
            var sector = await repo.GetByIdAsync(id);
            if (sector == null) return false;
            if (!string.IsNullOrEmpty(sector.ImagePublicId))
            {
                await cloudinaryService.DeleteImageAsync(sector.ImagePublicId);
            }
            await repo.DeleteAsync(sector);
            return true;
        }

        public async Task<List<getSectorDTO>> GetAll()
        {
            var sectors = await repo.GetAllAsync();
            var mappedSectors = new List<getSectorDTO>();

            var relatedEmployees = sectors.Where(s => s.addedBy.HasValue).Select(s => s.addedBy.Value).Distinct().ToList();
            var allEmployees = await employeeService.GetByIds(relatedEmployees);
            var employeeDictionary = allEmployees.ToDictionary(e => e.Id);

            foreach (var sector in sectors)
            {
                var dto = mapper.Map<getSectorDTO>(sector);
                if (sector.addedBy.HasValue)
                {
                    if (employeeDictionary.TryGetValue(sector.addedBy.Value, out var employee))
                    {
                        var mappedEmployee = mapper.Map<getBlogUserDTO>(employee);
                        dto.author = mappedEmployee;
                    }
                }
                dto.RelatedProductsCount = sector.Related_Projects?.Count ?? 0;
                mappedSectors.Add(dto);
            }

            return mappedSectors;
        }

        public async Task<getSectorDTO> GetById(int id)
        {
            var sector = await repo.GetByIdAsync(id);
            if (sector == null) return null;
            var dto = mapper.Map<getSectorDTO>(sector);
            if (sector.addedBy.HasValue)
            {
                var employee = await employeeService.GetById(sector.addedBy.Value);
                if (employee != null)
                {
                    dto.author = mapper.Map<getBlogUserDTO>(employee);
                }
            }
            dto.RelatedProductsCount = sector.Related_Projects?.Count ?? 0;
            return dto;
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
            if (sectorDTO.image != null)
            {
                var uploadResult = await cloudinaryService.UpdateImageAsync(recorded.ImagePublicId, sectorDTO.image, "Sectors");
                if (uploadResult != null)
                {
                    recorded.ImagePublicId = uploadResult.PublicId;
                    recorded.ImageUrl = uploadResult.Url;
                }
            }

            repo.UpdateAsync(recorded);
            return true;
        }

        public async Task<List<Sector>> GetByIdsAsync(List<int> ids)
        {
            var result = new List<Sector>();
            if (ids == null || ids.Count == 0) return result;

            foreach (var id in ids)
            {
                var sector = await repo.GetByIdAsync(id);
                if (sector != null)
                    result.Add(sector);
            }

            return result;
        }
    }
}
