using Deep_lines_Backend.Domain.Models.SharedDTOs.CloudinaryDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float? price { get; set; } = null;

        public string? ImagePublicId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime published_data {  get; set; } = DateTime.Now;
        public List<Order>? orders {  get; set; } = new List<Order>();
        public List<Review>? reviwes { get; set; } = new List<Review>();
        public int SectorId { get; set; }
        public Sector? sector { get; set; }

        public int? addedBy { get; set; }
        public int? updatedBy { get; set; }


    }
}
