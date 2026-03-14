using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.BlogEntity
{
    public class AddBlogDTO
    {
        public string title { get; set; }
        public string content { get; set; }
        public IFormFile? image { get; set; } = null;
        public DateTime published_date { get; set; } = DateTime.Now;
        public int? User_Id { get; set; }
        public int? updatedBy { get; set; }
    }
}
