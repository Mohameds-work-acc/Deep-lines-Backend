using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Deep_lines_Backend.BLL.JWT
{
    public class JWTService : IJWTService
    {
        private readonly JWTConfig jWTConfig;
        private readonly IUserService userService;
        private readonly IRefreshTokenRepo refreshTokenRepo;
        public JWTService(IOptions<JWTConfig> jwtConfig , IRefreshTokenRepo refreshTokenRepo , IUserService userService )
        {
            this.jWTConfig = jwtConfig.Value;
            this.refreshTokenRepo = refreshTokenRepo;
            this.userService = userService;
        }

        public string GenerateRefreshToken()
        {
            
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }

        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTConfig.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jWTConfig.Issuer,
                audience: jWTConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jWTConfig.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public RefreshTokenResponse? RefreshToken(int userID , string refreshToken)
        {
            var refreshTokenResponse = new RefreshTokenResponse();

            var recordedToken = refreshTokenRepo.CheckRefreshToken(userID, refreshToken);
            if (recordedToken == null)
            {
                return null;
            }

            refreshTokenRepo.removeRefreshToken(recordedToken.Id);

            var user = userService.GetById(userID).Result;

            refreshTokenResponse.JWTToken = GenerateToken(user);

            refreshTokenResponse.JWTTokenExpires = DateTime.Now.AddMinutes(jWTConfig.ExpiresInMinutes);

            refreshTokenResponse.RefreshToken = GenerateRefreshToken();

            refreshTokenResponse.RefreshTokenExpires = DateTime.Now.AddDays(jWTConfig.RefreshTokenExpiresInDays);

            return refreshTokenResponse;


        }
    }
}
