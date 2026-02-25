using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.DTOs.AuthServices
{
    public class RefreshTokenResponse
    {
        public string JWTToken { get; set; }
        public DateTime JWTTokenExpires { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }

    }
}
