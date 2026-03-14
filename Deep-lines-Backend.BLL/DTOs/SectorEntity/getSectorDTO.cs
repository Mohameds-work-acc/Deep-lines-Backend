using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.SectorEntity
{
    public class getSectorDTO
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string? ImagePublicId { get; set; }
        public string? ImageUrl { get; set; }
        public string vision { get; set; }
        public string mission { get; set; }
        public getBlogUserDTO author { get; set; }
        public getBlogUserDTO? updatedBy { get; set; }
        public int RelatedProductsCount { get; set; }
    }
}
