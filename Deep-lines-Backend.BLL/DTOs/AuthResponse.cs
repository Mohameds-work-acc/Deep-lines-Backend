using Deep_lines_Backend.BLL.DTOs.AuthServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Deep_lines_Backend.BLL.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        [JsonIgnore]
        public string refreshToken { get; set; }
        public DateTime refreshTokenExpiration { get; set; }
        public UserInfoDTO User { get; set; } = new UserInfoDTO();
        public string Message { get; set; } = string.Empty;
    }
}
