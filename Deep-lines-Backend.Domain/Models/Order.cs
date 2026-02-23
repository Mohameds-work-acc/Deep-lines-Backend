using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string detailed_address { get; set; }
        public string comment { get; set; }
        public string Country_And_State { get; set; }
        public Customer Customer { get; set; }

    }
}
