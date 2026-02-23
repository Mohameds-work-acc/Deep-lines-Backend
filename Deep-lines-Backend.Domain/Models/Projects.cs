using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Projects
    {
        public int Id { get; set; }
        public string description { get; set; }
        public string image_url {  get; set; }
        public int? User_Id { get; set; }
        public User? user { get; set; }
        public int? Sector_Id { get; set; }
        public Sector? sector { get; set; }
    }
}
