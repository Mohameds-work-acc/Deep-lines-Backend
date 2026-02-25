using Deep_lines_Backend.BLL.DTOs.AuthServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IAuthService
    {
        public AuthResponse? Authenticate(LoginDTO loginDTO);
        public RefreshTokenResponse RefreshToken(int id ,  string refreshToken);

    }
}
