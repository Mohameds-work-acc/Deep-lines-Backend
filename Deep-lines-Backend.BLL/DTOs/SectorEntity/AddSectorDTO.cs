using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.SectorEntity
{
    public class AddSectorDTO
    {
        public string title { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public string vision { get; set; }
        public string mission { get; set; }
        public int? User_Id { get; set; }
    }
}
