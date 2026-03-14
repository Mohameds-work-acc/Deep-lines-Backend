using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.SectorEntity
{
    public class AddSectorDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public IFormFile? image { get; set; } = null;
        public string vision { get; set; }
        public string mission { get; set; }
        public int? User_Id { get; set; }
    }
}
