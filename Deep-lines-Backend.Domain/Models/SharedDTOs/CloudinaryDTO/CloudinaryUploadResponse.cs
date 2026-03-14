using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Deep_lines_Backend.Domain.Models.SharedDTOs.CloudinaryDTO
{
    public class CloudinaryUploadResponse
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
    }
}
