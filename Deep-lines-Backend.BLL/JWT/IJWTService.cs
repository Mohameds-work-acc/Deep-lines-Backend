using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.JWT
{
    public interface IJWTService
    {
        public string GenerateToken(Employee user);
        public string GenerateRefreshToken();
        public RefreshTokenResponse? RefreshToken(int userID , string refreshToken);
    }
}
