using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.ProductEntity;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Deep_lines_Backend.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepo<Product> repo;
        private readonly IGenericRepo<Sector> sectorRepo;
        private readonly ISectorService sectorService;
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;
        private readonly CloudinaryService cloudinaryService;

        public ProductService(IGenericRepo<Product> repo, IGenericRepo<Sector> sectorRepo, IEmployeeService employeeService, IMapper mapper, CloudinaryService cloudinaryService, ISectorService sectorService)
        {
            this.repo = repo;
            this.sectorRepo = sectorRepo;
            this.employeeService = employeeService;
            this.mapper = mapper;
            this.cloudinaryService = cloudinaryService;
            this.sectorService = sectorService;
        }

        public async Task AddProduct(AddProductDTO productDTO)
        {
            var mapped = mapper.Map<Product>(productDTO);
            if (productDTO.Sector_Id != 0)
            {
                var sector = await sectorRepo.GetByIdAsync(productDTO.Sector_Id);
                mapped.sector = sector;
            }
            if (productDTO.image != null)
            {
                var uploadResult = await cloudinaryService.UploadImageAsync(productDTO.image, "Products");
                if (uploadResult != null)
                {
                    mapped.ImagePublicId = uploadResult.PublicId;
                    mapped.ImageUrl = uploadResult.Url;
                }
            }

            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return false;
            if (!string.IsNullOrEmpty(product.ImagePublicId))
            {
                await cloudinaryService.DeleteImageAsync(product.ImagePublicId);
            }
            await repo.DeleteAsync(product);
            return true;
        }

        public async Task<List<getProductDTO>> GetAll()
        {
            var products = await repo.GetAllAsync();
            var mappedProducts = new List<getProductDTO>();

            var relatedEmployees = products.Where(p => p.addedBy.HasValue).Select(p => p.addedBy.Value).Distinct().ToList();
            var allEmployees = await employeeService.GetByIds(relatedEmployees);
            var employeeDictionary = allEmployees.ToDictionary(e => e.Id);
            var sectors = products.Where(p => p.SectorId != null).Select(p => p.SectorId).Distinct().ToList();
            var allSectors = await sectorService.GetByIdsAsync(sectors);
            var sectorDictionary = allSectors.ToDictionary(s => s.Id);

            foreach (var product in products)
            {
                var dto = mapper.Map<getProductDTO>(product);
               
                if (product.addedBy.HasValue)
                {
                    if (employeeDictionary.TryGetValue(product.addedBy.Value, out var employee))
                    {
                        var mappedEmployee = mapper.Map<getBlogUserDTO>(employee);
                        dto.author = mappedEmployee;
                    }
                    if (sectorDictionary.TryGetValue(product.sector.Id, out var sector))
                    {
                        var sectorDto = new getSectorWithProductDTO
                        {
                            Id = sector.Id,
                            title = sector.title
                        };
                        dto.sector = sectorDto;
                    }
                }
                mappedProducts.Add(dto);
            }

            return mappedProducts;
        }

        public async Task<getProductDTO> GetById(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return null;
            var dto = mapper.Map<getProductDTO>(product);
            if (product.addedBy.HasValue)
            {
                var employee = await employeeService.GetById(product.addedBy.Value);
                if (employee != null)
                {
                    dto.author = mapper.Map<getBlogUserDTO>(employee);
                }
            }
            return dto;
        }

        public async Task<bool> UpdateProduct(AddProductDTO productDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(productDTO, recorded);
            if (productDTO.Sector_Id != 0)
            {
                var sector = await sectorRepo.GetByIdAsync(productDTO.Sector_Id);
                recorded.sector = sector;
            }
            if(productDTO.updatedBy.HasValue)
            {
                recorded.updatedBy = productDTO.updatedBy.Value;
            }
            if (productDTO.image != null)
            {
                var uploadResult = await cloudinaryService.UpdateImageAsync(recorded.ImagePublicId, productDTO.image, "Products");
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
