using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.ReviewEntity
{
    public class AddReviewDTO
    {
        public int rate { get; set; }
        public string comment { get; set; }
        public string reviewer_name { get; set; }
        public string reviewer_email { get; set; }
        public int Product_Id { get; set; }
    }
}
