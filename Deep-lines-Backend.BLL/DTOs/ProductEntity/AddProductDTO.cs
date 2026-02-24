using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.ProductEntity
{
    public class AddProductDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public DateTime published_data { get; set; } = DateTime.Now;
        public int? User_Id { get; set; }
        public int Category_Id { get; set; }
    }
}
