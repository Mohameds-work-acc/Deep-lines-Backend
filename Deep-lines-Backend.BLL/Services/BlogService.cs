using AutoMapper;
using CloudinaryDotNet;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Deep_lines_Backend.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly CloudinaryService cloudinaryService;
        private readonly IEmployeeService employeeService;
        private readonly IGenericRepo<Blog> repo;
        private readonly IMapper mapper;
        
        public BlogService(IGenericRepo<Blog> repo, IMapper mapper , IEmployeeService employeeService , CloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
            this.repo = repo;
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        public async Task AddBlog(AddBlogDTO blogDTO)
        {
            var mappedBlog = mapper.Map<Blog>(blogDTO);

            var uploadResult = await cloudinaryService.UploadImageAsync(blogDTO.image , "Blogs");

            if (uploadResult != null)
            {
                mappedBlog.ImagePublicId = uploadResult.PublicId;
                mappedBlog.ImageUrl = uploadResult.Url;
            }

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
            if (!string.IsNullOrEmpty(blog.ImagePublicId))
            {
                await cloudinaryService.DeleteImageAsync(blog.ImagePublicId);
            }
            await repo.DeleteAsync(blog);
            return true;
        }

        public async Task<List<getBlogDTO>> GetAll()
        {
            var blogs = await repo.GetAllWithIncludesAsync(c=>c.comments);
            // Map each blog individually to capture detailed mapping errors and avoid bulk mapping issues
            var mappedBlogs = new List<getBlogDTO>();
            var relatedEmployees = blogs.Where(b => b.addedBy.HasValue).Select(b => b.addedBy.Value).Distinct().ToList();
            var allEmployees = await employeeService.GetByIds(relatedEmployees);
            var employeeDictionary = allEmployees.ToDictionary(e => e.Id); 
            foreach (var blog in blogs)
            {
                try
                {
                    var dto = mapper.Map<getBlogDTO>(blog);
                    if (blog.addedBy.HasValue)
                    {
                        if (employeeDictionary.TryGetValue(blog.addedBy.Value, out var employee))
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
            var blogs = await repo.GetAllWithIncludesAsync(c => c.comments);
            var blog = blogs.FirstOrDefault(b => b.Id == id);
            return blog;
        }

        public async Task<bool> UpdateBlog(AddBlogDTO blogDTO, int id)
        {
            var recorded_blog = await repo.GetByIdAsync(id);

            if (recorded_blog == null) return false;

            mapper.Map(blogDTO, recorded_blog);
            if (blogDTO.User_Id.HasValue && blogDTO.User_Id.Value != 0)
            {
                recorded_blog.updatedBy = blogDTO.updatedBy;
            }
            else
            {
                recorded_blog.updatedBy = null;
            }

            if (blogDTO.image != null)
            {
                var uploadResult = await cloudinaryService.UpdateImageAsync(recorded_blog.ImagePublicId , blogDTO.image, "Blogs");
                if (uploadResult != null)
                {
                    recorded_blog.ImagePublicId = uploadResult.PublicId;
                    recorded_blog.ImageUrl = uploadResult.Url;
                }
            }

             repo.UpdateAsync(recorded_blog);
             
            return true;

        }
    }
}
