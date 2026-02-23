using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.BlogEntity
{
    public class AddBlogDTO
    {
        public string title { get; set; }
        public string content { get; set; }
        public string imageUrl { get; set; }
        public DateTime published_date { get; set; } = DateTime.Now;
        public int User_Id { get; set; }
    }
}
