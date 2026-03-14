using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Deep_lines_Backend.BLL.DTOs.AuthServices
{
    public class AuthResponse
    {
        public TokensResponse? tokens { get; set; } = new TokensResponse();
        public string Message { get; set; } = string.Empty;
    }
}
