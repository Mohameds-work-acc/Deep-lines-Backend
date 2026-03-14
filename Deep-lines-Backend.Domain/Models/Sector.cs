using Deep_lines_Backend.Domain.Models.SharedDTOs.CloudinaryDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Sector
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string? ImagePublicId { get; set; }
        public string? ImageUrl { get; set; }
        public string vision {  get; set; }
        public string mission { get; set;  }

        public int? addedBy { get; set; }
        public int? updatedBy { get; set; }

        public List<Projects>? Related_Projects { get; set; } = new List<Projects>();

    }
}
