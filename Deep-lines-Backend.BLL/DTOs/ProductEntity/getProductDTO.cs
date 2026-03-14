using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.ProductEntity
{
    public class getProductDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public string? ImagePublicId { get; set; }
        public string? ImageUrl { get; set; }
        public getBlogUserDTO author { get; set; }
        public getBlogUserDTO? updatedBy { get; set; }
        public DateTime published_data { get; set; } = DateTime.Now;
        public getSectorWithProductDTO sector { get; set; }
    }
    public class getSectorWithProductDTO
    {
           public int Id { get; set; }
           public string title { get; set; }

    }
}
