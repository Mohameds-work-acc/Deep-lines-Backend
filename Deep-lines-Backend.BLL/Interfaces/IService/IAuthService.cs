using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.DTOs.SystemDTOs;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IAuthService
    {
        public Task<AuthResponse?> Authenticate(LoginDTO loginDTO);
        public TokensResponse RefreshToken(int id ,  string refreshToken);
        public Task<failResponse>? ChangePassword(ChangePasswordDTO changePasswordDTO);
        public Task<failResponse>? resetPasswordToDefult(int id);
        public Task<failResponse>? deleteRefreshToken(string token);

    }
}
