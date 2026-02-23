using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.Interfaces;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IGenericRepo<Blog> repo;
        private readonly IMapper mapper;
        
        public BlogService(IGenericRepo<Blog> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task AddBlog(AddBlogDTO blogDTO)
        {
            var mappedBlog = mapper.Map<Blog>(blogDTO);
            await repo.AddAsync(mappedBlog);
        }

        public async Task<bool> DeleteBlog(int id)
        {
            var blog = await repo.GetByIdAsync(id);
            if (blog == null) return false;
            await repo.DeleteAsync(blog);
            return true;
        }

        public Task<List<Blog>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<Blog> GetById(int id)
        {
            var blog = await repo.GetByIdAsync(id);
            return blog;
        }

        public async Task<bool> UpdateBlog(AddBlogDTO blogDTO, int id)
        {
            var recorded_blog = await repo.GetByIdAsync(id);

            if (recorded_blog == null) return false;

            mapper.Map(blogDTO, recorded_blog);

            await repo.UpdateAsync(recorded_blog);
             
            return true;

        }
    }
}
