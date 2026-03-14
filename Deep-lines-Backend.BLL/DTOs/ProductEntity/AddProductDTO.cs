using Microsoft.AspNetCore.Http;
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
        public IFormFile? image { get; set; } = null;
       
        public DateTime published_data { get; set; } = DateTime.Now;
        public int? addedBy { get; set; }
        public int? updatedBy { get; set; }
        public int Sector_Id { get; set; }  
    }
}
