using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public List<Projects>? Published_Projects { get; set; } = new List<Projects>();
        public List<Blog>? Published_Blogs { get; set; } = new List<Blog>();
        public List<Sector>? Published_Sectors { get; set; } = new List<Sector>();
        public List<Product>? Published_Products { get; set; } = new List<Product>();

    }
}
