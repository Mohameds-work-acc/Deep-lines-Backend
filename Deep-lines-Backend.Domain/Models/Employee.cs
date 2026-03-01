using Deep_lines_Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
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
        public List<RefreshToken>? refreshTokens { get; set; }= new List<RefreshToken>(); 

    }
}
