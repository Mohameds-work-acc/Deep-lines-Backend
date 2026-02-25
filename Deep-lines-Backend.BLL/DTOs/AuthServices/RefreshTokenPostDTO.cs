using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.AuthServices
{
    public class RefreshTokenPostDTO
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
