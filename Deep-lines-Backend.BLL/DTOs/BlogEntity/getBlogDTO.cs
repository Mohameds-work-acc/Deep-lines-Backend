using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.BlogEntity
{
    public class getBlogDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string imageUrl { get; set; }
        public getBlogUserDTO author { get; set; } 
        public getBlogUserDTO? updatedBy { get; set; }
        public DateTime published_date { get; set; } = DateTime.Now;
        public List<Comment>? comments { get; set; } = new List<Comment>();
    }
    public class getBlogUserDTO
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}
