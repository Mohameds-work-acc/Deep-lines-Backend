using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.ProductEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepo<Product> repo;
        private readonly IGenericRepo<Category> categoryRepo;
        private readonly IMapper mapper;

        public ProductService(IGenericRepo<Product> repo, IGenericRepo<Category> categoryRepo, IMapper mapper)
        {
            this.repo = repo;
            this.categoryRepo = categoryRepo;
            this.mapper = mapper;
        }

        public async Task AddProduct(AddProductDTO productDTO)
        {
            var mapped = mapper.Map<Product>(productDTO);
            if (productDTO.Category_Id != 0)
            {
                var category = await categoryRepo.GetByIdAsync(productDTO.Category_Id);
                mapped.category = category;
            }
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return false;
            await repo.DeleteAsync(product);
            return true;
        }

        public Task<List<Product>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateProduct(AddProductDTO productDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(productDTO, recorded);
            if (productDTO.Category_Id != 0)
            {
                var category = await categoryRepo.GetByIdAsync(productDTO.Category_Id);
                recorded.category = category;
            }
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
