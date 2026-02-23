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

namespace Deep_lines_Backend.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddBlogDTO, Blog>().ReverseMap();
            CreateMap<AddUserDTO, User>().ReverseMap();
            CreateMap<AddCategoryDTO, Category>().ReverseMap();
            CreateMap<AddCommentDTO, Comment>().ReverseMap();
            CreateMap<AddCustomerDTO, Customer>().ReverseMap();
        }
    }
}
