using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.ProjectsEntity
{
    public class AddProjectsDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public IFormFile? image { get; set; } = null;

        public int? User_Id { get; set; }
        public int? Sector_Id { get; set; }
    }
}
