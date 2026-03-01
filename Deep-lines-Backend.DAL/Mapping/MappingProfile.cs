using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using Deep_lines_Backend.BLL.DTOs.CategoryEntity;
using Deep_lines_Backend.DAL.Models;
using Deep_lines_Backend.BLL.DTOs.CommentEntity;
using Deep_lines_Backend.BLL.DTOs.CustomerEntity;
using Deep_lines_Backend.BLL.DTOs.OrderEntity;
using Deep_lines_Backend.BLL.DTOs.ProductEntity;
using Deep_lines_Backend.BLL.DTOs.ProjectsEntity;
using Deep_lines_Backend.BLL.DTOs.ReviewEntity;
using Deep_lines_Backend.BLL.DTOs.SectorEntity;

namespace Deep_lines_Backend.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBlogDTO, Blog>().ReverseMap();
            CreateMap<AddUserDTO, Employee>().ReverseMap();
            CreateMap<AddCategoryDTO, Category>().ReverseMap();
            CreateMap<AddCommentDTO, Comment>().ReverseMap();
            CreateMap<AddCustomerDTO, Customer>().ReverseMap();
            CreateMap<AddOrderDTO, Order>().ReverseMap();
            CreateMap<AddSectorDTO, Sector>().ReverseMap();
            CreateMap<AddProductDTO, Product>().ReverseMap();
            CreateMap<AddReviewDTO, Review>().ReverseMap();
            CreateMap<AddProjectsDTO, Projects>().ReverseMap();
        }
    }
}
