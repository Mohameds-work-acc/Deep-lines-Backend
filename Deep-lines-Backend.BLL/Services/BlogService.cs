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
        private readonly IEmployeeService employeeService;
        private readonly IGenericRepo<Blog> repo;
        private readonly IMapper mapper;
        
        public BlogService(IGenericRepo<Blog> repo, IMapper mapper , IEmployeeService employeeService)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        public async Task AddBlog(AddBlogDTO blogDTO)
        {
            var mappedBlog = mapper.Map<Blog>(blogDTO);
            // ensure we don't set an invalid FK (0)
            if (blogDTO.User_Id.HasValue && blogDTO.User_Id.Value != 0)
            {
                mappedBlog.addedBy = blogDTO.User_Id;
            }
            else
            {
                mappedBlog.addedBy = null;
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

        public async Task<List<getBlogDTO>> GetAll()
        {
            var blogs = await repo.GetAllAsync();
            // Map each blog individually to capture detailed mapping errors and avoid bulk mapping issues
            var mappedBlogs = new List<getBlogDTO>();
            foreach (var blog in blogs)
            {
                try
                {
                    var dto = mapper.Map<getBlogDTO>(blog);

                    if (blog.addedBy.HasValue)
                    {
                        var employee = await employeeService.GetById(blog.addedBy.Value);
                        if (employee != null)
                        {
                            var mappedEmployee = mapper.Map<getBlogUserDTO>(employee);
                            dto.author = mappedEmployee;
                        }
                    }

                    mappedBlogs.Add(dto);
                }
                catch (AutoMapperMappingException ex)
                {
                    // Rethrow with context about which Blog failed to map to help diagnose root cause
                    throw new AutoMapperMappingException($"Failed mapping Blog (Id={blog?.Id.ToString() ?? "null"}) to getBlogDTO.", ex);
                }
            }

            return mappedBlogs;
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
                recorded_blog.addedBy = blogDTO.User_Id;
            }
            else
            {
                recorded_blog.addedBy = null;
            }

            await repo.UpdateAsync(recorded_blog);
             
            return true;

        }
    }
}
