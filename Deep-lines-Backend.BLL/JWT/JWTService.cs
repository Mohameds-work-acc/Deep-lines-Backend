using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Deep_lines_Backend.Domain.Models;
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
        private readonly IEmployeeService userService;
        private readonly IRefreshTokenRepo refreshTokenRepo;
        public JWTService(IOptions<JWTConfig> jwtConfig , IRefreshTokenRepo refreshTokenRepo , IEmployeeService userService )
        {
            this.jWTConfig = jwtConfig.Value;
            this.refreshTokenRepo = refreshTokenRepo;
            this.userService = userService;
        }

        public string GenerateRefreshToken(int userId)
        {
            
            var randomNumber = new byte[32];

            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            var tokenString = Convert.ToBase64String(randomNumber);

            var refreshToken = new RefreshToken
            {
                    createdOn = DateTime.UtcNow,
                    Expiration = DateTime.UtcNow.AddDays(jWTConfig.RefreshTokenExpiresInDays),
                    Token = tokenString,
                    UserId = userId,
                    
            };

            var refreshTokenRelated = refreshTokenRepo.getRefreshTokenList(userId);

            foreach (var rToken in refreshTokenRelated)
            {
                refreshTokenRepo.removeRefreshToken(rToken.Token);
            }
            

            refreshTokenRepo.addRefreshToken(refreshToken);
            return tokenString;
            

        }

        public string GenerateToken(Employee user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Name, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Role, user.department ?? string.Empty),
                new Claim("department", user.department ?? string.Empty),
                new Claim("jopTitle", user.jopTitle ?? string.Empty),
                new Claim("status", user.status ?? string.Empty)
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

        public TokensResponse? RefreshToken(int userID , string refreshToken)
        {
            var refreshTokenResponse = new TokensResponse();

            var recordedToken = refreshTokenRepo.CheckRefreshToken(userID, refreshToken);
            if (recordedToken == null)
            {
                return null;
            }

            refreshTokenRepo.removeRefreshToken(recordedToken.RefreshToken);

            var user = userService.GetById(userID).Result;

            refreshTokenResponse.JWTToken = GenerateToken(user);

            refreshTokenResponse.JWTTokenExpires = DateTime.Now.AddMinutes(jWTConfig.ExpiresInMinutes);

            refreshTokenResponse.RefreshToken = GenerateRefreshToken(userID);

            refreshTokenResponse.RefreshTokenExpires = DateTime.Now.AddDays(jWTConfig.RefreshTokenExpiresInDays);

            return refreshTokenResponse;


        }
    }
}
