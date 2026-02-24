using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.OrderEntity
{
    public class AddOrderDTO
    {
        public string City { get; set; }
        public string detailed_address { get; set; }
        public string comment { get; set; }
        public string Country_And_State { get; set; }
        public int Customer_Id { get; set; }
    }
}
