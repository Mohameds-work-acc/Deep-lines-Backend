using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.Domain.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsActive => DateTime.UtcNow < Expiration || revokedOn != null;
        public DateTime createdOn { get; set; } = DateTime.UtcNow;
        public DateTime? revokedOn { get; set; } = null;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
