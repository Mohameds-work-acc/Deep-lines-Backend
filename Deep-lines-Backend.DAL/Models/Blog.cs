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
        public DateTime published_date { get; set; } = DateTime.Now;
        public int User_Id { get; set; }
        public User user { get; set; }
        public List<Comment> comments { get; set; }
    }
}
