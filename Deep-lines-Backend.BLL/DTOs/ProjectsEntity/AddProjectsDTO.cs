using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.ProjectsEntity
{
    public class AddProjectsDTO
    {
        public string description { get; set; }
        public string image_url { get; set; }
        public int? User_Id { get; set; }
        public int? Sector_Id { get; set; }
    }
}
