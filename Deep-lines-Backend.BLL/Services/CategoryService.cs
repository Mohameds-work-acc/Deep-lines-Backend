using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.CategoryEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepo<Category> repo;
        private readonly IMapper mapper;

        public CategoryService(IGenericRepo<Category> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task AddCategory(AddCategoryDTO categoryDTO)
        {
            var mapped = mapper.Map<Category>(categoryDTO);
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteBlog(int id)
        {
            var category = await repo.GetByIdAsync(id);
            if (category == null) return false;
            await repo.DeleteAsync(category);
            return true;
        }

        public Task<List<Category>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<bool> UpdateCategory(AddCategoryDTO categoryDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(categoryDTO, recorded);
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
