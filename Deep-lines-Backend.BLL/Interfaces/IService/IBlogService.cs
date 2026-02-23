using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IBlogService
    {
        public Task AddBlog(AddBlogDTO blogDTO);
        public Task<Blog> GetById(int id);
        public Task<List<Blog>> GetAll();
        public Task<bool> UpdateBlog(AddBlogDTO blogDTO , int id);
        public Task<bool> DeleteBlog(int id);

    }
}
