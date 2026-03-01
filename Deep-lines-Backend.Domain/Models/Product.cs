using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public DateTime published_data {  get; set; } = DateTime.Now;
        public List<Order>? orders {  get; set; } = new List<Order>();
        public List<Review>? reviwes { get; set; } = new List<Review>();
        public Category category { get; set; }

        public int? addedBy { get; set; }
        public int? updatedBy { get; set; }


    }
}
