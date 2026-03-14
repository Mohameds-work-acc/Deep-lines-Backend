using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.ProjectsEntity
{
    public class getProjectsDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string? ImagePublicId { get; set; }
        public string? ImageUrl { get; set; }
        public getSectorWithProjectDTO? sector { get; set; }
        public getBlogUserDTO author { get; set; }
        public getBlogUserDTO? updatedBy { get; set; }
    }
    public class getSectorWithProjectDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
    }
}
