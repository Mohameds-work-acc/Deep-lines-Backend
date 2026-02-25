using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.BLL.JWT;
using Deep_lines_Backend.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly IJWTService jwtService;
        private readonly JWTConfig jWTConfig;
        private readonly PasswordHasher<object> passwordHasher = new();

        public AuthService(IUserService userService, IJWTService jwtService , IOptions<JWTConfig> options)
        {
            this.userService = userService;
            this.jwtService = jwtService;
            this.jWTConfig = options.Value;
        }

        public AuthResponse? Authenticate(LoginDTO loginDTO)
        {
            if (loginDTO == null || string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
                return null;

            var user = userService.GetByEmail(loginDTO.Email).Result;
            if (user == null || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }

            var verificationFailed = passwordHasher.VerifyHashedPassword(null, user.Password, loginDTO.Password) == PasswordVerificationResult.Failed;
            if (verificationFailed)
            {
                return null;
            }
            return new AuthResponse
            {
                Token = jwtService.GenerateToken(user),
                refreshToken = new RefreshTokenDTO
                {
                    Token = jwtService.GenerateRefreshToken(),
                    ExpiryDate = DateTime.UtcNow.AddDays(jWTConfig.RefreshTokenExpiresInDays)
                },
                User = new UserInfoDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role
                },
                Message = "Authentication successful"
            };
        }

        public RefreshTokenResponse RefreshToken(int id , string refreshToken)
        {
            var tokenResponse = jwtService.RefreshToken(id, refreshToken);
            if (tokenResponse == null)
            {
                return null;
            }
            return tokenResponse;
        }
    }
}
