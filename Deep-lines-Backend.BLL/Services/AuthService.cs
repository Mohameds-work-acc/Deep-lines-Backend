using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.DTOs.EmailDTOs;
using Deep_lines_Backend.BLL.DTOs.SystemDTOs;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.BLL.JWT;
using Deep_lines_Backend.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IWebHostEnvironment env;
        private readonly IEmailService emailService;
        private readonly IRefreshTokenRepo refreshTokenRepo;
        private readonly IEmployeeService userService;
        private readonly IJWTService jwtService;
        private readonly IGenericRepo<Employee> repo;
        private readonly IConfiguration configuration;
        private readonly JWTConfig jWTConfig;
        private readonly PasswordHasher<object> passwordHasher = new();

        public AuthService(IWebHostEnvironment env, IEmailService emailService, IRefreshTokenRepo refreshTokenRepo, IEmployeeService userService, IJWTService jwtService , IOptions<JWTConfig> options , IGenericRepo<Employee> repo , IConfiguration configuration)
        {
            this.env = env;
            this.emailService = emailService;
            this.refreshTokenRepo = refreshTokenRepo;
            this.configuration = configuration;
            this.repo = repo;
            this.userService = userService;
            this.jwtService = jwtService;
            this.jWTConfig = options.Value;
        }

        public  AuthResponse? Authenticate(LoginDTO loginDTO)
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
            var sendEmailDTO = new sendEmailDTO
            {
                Email = loginDTO.Email,
                Body = "There is a new Login to your account",
                Subject = "New Login Successfully"
            };
            emailService.sendEmail(sendEmailDTO);
            var authResponse = new AuthResponse
            {
                Message = "Login successful",
                tokens =
                {
                    JWTToken = jwtService.GenerateToken(user),
                    RefreshToken = jwtService.GenerateRefreshToken(user.Id),
                    JWTTokenExpires = DateTime.UtcNow.AddMinutes(jWTConfig.ExpiresInMinutes),
                    RefreshTokenExpires = DateTime.UtcNow.AddDays(jWTConfig.RefreshTokenExpiresInDays)
                }
            };
            return authResponse;
            
        }

        public TokensResponse RefreshToken(int id , string refreshToken)
        {
            var tokenResponse = jwtService.RefreshToken(id, refreshToken);
            if (tokenResponse == null)
            {
                return null;
            }
            return tokenResponse;
        }
        public Task<failResponse>? ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var recorded = userService.GetById(changePasswordDTO.userId).Result;
            if (recorded == null)
            {
                var failResponse = new failResponse
                {
                    code = 404,
                    message = "User not found"
                };
                return Task.FromResult<failResponse?>(failResponse);
            }
            var verificationResult = passwordHasher.VerifyHashedPassword(null, recorded.Password, changePasswordDTO.oldPassword);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                var failResponse = new failResponse
                {
                    code = 400,
                    message = "Old password is incorrect"
                };
                return Task.FromResult<failResponse?>(failResponse);
            }

            recorded.Password = passwordHasher.HashPassword(null, changePasswordDTO.newPassword);

            repo.UpdateAsync(recorded);

            return Task.FromResult<failResponse?>(null);

        }

        public async Task<failResponse>? resetPasswordToDefult(int id)
        {
            var recorded = userService.GetById(id).Result;
            if (recorded == null)
            {
                var failResponse = new failResponse
                {
                    code = 404,
                    message = "User not found"
                };
                return failResponse;
            }
            recorded.Password = passwordHasher.HashPassword(null, configuration["Employee:DefaultPassword"]);
            repo.UpdateAsync(recorded);

            var path = Path.Combine(
                env.ContentRootPath,
                "Templates",
                "password-reset.html"
                );
            var template = System.IO.File.ReadAllText(path);
            var emailBody =  template.Replace("{{employee_name}}", recorded.Name)
                            .Replace("{{temporary_password}}", configuration["Employee:DefaultPassword"])
                            .Replace("{{support_email}}", "support@test.com")
                            .Replace("{{company_name}}", "Deep-lines")
                            .Replace("{{current_year}}", DateTime.UtcNow.Year.ToString());
            var sendEmailDTO = new sendEmailDTO {

                Email = recorded.Email,
                Subject = "Password Reset Notification",
                Body = emailBody

            };
            await emailService.sendEmail(sendEmailDTO);

            return null;
        }

        public Task<failResponse>? deleteRefreshToken(string token)
        {
            refreshTokenRepo.removeRefreshToken(token);
            return Task.FromResult<failResponse?>(null);
        }
    }
}
