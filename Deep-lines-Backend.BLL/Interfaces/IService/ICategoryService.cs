using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.DTOs.CategoryEntity;
using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface ICategoryService
    {
        public Task AddCategory(AddCategoryDTO categoryDTO);
        public Task<List<Category>> GetAll();
        public Task<bool> UpdateCategory(AddCategoryDTO categoryDTO, int id);
        public Task<bool> DeleteBlog(int id);
    }
}
