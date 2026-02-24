using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string imageUrl { get; set; }
        public DateTime published_date { get; set; } = DateTime.Now;
        public int? User_Id { get; set; } = null;
        public User? user { get; set; } = null;
        public List<Comment>? comments { get; set; } = new List<Comment>();
    }
}
