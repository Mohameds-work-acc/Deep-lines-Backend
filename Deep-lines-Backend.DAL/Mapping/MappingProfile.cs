using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using Deep_lines_Backend.DAL.Models;
using Deep_lines_Backend.BLL.DTOs.CommentEntity;
using Deep_lines_Backend.BLL.DTOs.CustomerEntity;
using Deep_lines_Backend.BLL.DTOs.OrderEntity;
using Deep_lines_Backend.BLL.DTOs.ProductEntity;
using Deep_lines_Backend.BLL.DTOs.ProjectsEntity;
using Deep_lines_Backend.BLL.DTOs.ReviewEntity;
using Deep_lines_Backend.BLL.DTOs.SectorEntity;
using Deep_lines_Backend.Domain.Models.SharedDTOs.CloudinaryDTO;

namespace Deep_lines_Backend.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBlogDTO, Blog>()
                .ForMember(dest => dest.ImagePublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AddUserDTO, Employee>().ReverseMap();
            CreateMap<AddCommentDTO, Comment>().ReverseMap();
            CreateMap<AddCustomerDTO, Customer>().ReverseMap();
            CreateMap<AddOrderDTO, Order>().ReverseMap();
            CreateMap<AddSectorDTO, Sector>().ReverseMap();
            CreateMap<AddProductDTO, Product>().ReverseMap();
            CreateMap<AddReviewDTO, Review>().ReverseMap();
            CreateMap<AddProjectsDTO, Projects>().ReverseMap();
            // Explicit mapping for Blog <-> getBlogDTO
            // getBlogDTO.updatedBy is a complex DTO while Blog.updatedBy is an int?; ignore during mapping
            CreateMap<Blog, getBlogDTO>()
                .ForMember(dest => dest.author, opt => opt.Ignore())
                .ForMember(dest => dest.updatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Sector, getSectorDTO>()
                .ForMember(dest => dest.author, opt => opt.Ignore())
                .ForMember(dest => dest.updatedBy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Projects, getProjectsDTO>()
                .ForMember(dest => dest.author, opt => opt.Ignore())
                .ForMember(dest => dest.updatedBy, opt => opt.Ignore())
                .ForMember(dest=> dest.sector, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Product, getProductDTO>()
                .ForMember(dest => dest.author, opt => opt.Ignore())
                .ForMember(dest => dest.updatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.sector, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<getBlogUserDTO , Employee>().ReverseMap();
        }
    }
}
