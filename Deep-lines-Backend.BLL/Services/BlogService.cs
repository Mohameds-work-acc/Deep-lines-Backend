using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
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
            // ensure we don't set an invalid FK (0)
            if (blogDTO.User_Id.HasValue && blogDTO.User_Id.Value != 0)
            {
                mappedBlog.User_Id = blogDTO.User_Id;
            }
            else
            {
                mappedBlog.User_Id = null;
                mappedBlog.user = null;
            }

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
            if (blogDTO.User_Id.HasValue && blogDTO.User_Id.Value != 0)
            {
                recorded_blog.User_Id = blogDTO.User_Id;
            }
            else
            {
                recorded_blog.User_Id = null;
                recorded_blog.user = null;
            }

            await repo.UpdateAsync(recorded_blog);
             
            return true;

        }
    }
}
