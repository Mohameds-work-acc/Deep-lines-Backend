using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string comment { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
        public DateTime published_data { get; set; } = DateTime.Now;
        public int? BlogId { get; set; }
    }
}
