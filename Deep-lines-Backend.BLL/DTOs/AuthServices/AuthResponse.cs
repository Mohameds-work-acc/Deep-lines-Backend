using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Deep_lines_Backend.BLL.DTOs.AuthServices
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public RefreshTokenDTO refreshToken { get; set; }
        public UserInfoDTO User { get; set; } = new UserInfoDTO();
        public string Message { get; set; } = string.Empty;
    }
}
