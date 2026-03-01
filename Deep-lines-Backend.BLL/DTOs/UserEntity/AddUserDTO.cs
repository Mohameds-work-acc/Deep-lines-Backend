using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.UserEntity
{
    public class AddUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string status { get; set; }
        public string Role { get; set; }
        public string department { get; set; }
        public string jopTitle { get; set; }
        public string employmentType { get; set; }
        public DateTime joinedDate { get; set; }
    }
}
