using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.AuthServices
{
    public class RefreshTokenDTO
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
