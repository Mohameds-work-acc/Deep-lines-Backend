using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Sector
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public string vision {  get; set; }
        public string mission { get; set;  }
        public int? User_Id { get; set; }
        public User? published_user { get; set; }

        public List<Projects>? Related_Projects { get; set; } = new List<Projects>();

    }
}
