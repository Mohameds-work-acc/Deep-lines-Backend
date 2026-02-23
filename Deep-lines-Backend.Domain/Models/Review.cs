using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int rate { get; set; }
        public string comment { get; set; }
        public int Product_Id { get; set; }
        public Product product { get; set; }
    }
}
