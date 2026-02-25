using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService userService;
        private readonly PasswordHasher<object> passwordHasher = new();

        public bool Authenticate(LoginDTO loginDTO)
        {
            var user = userService.GetByEmail(loginDTO.Email).Result;
            var verificationResult = (passwordHasher.VerifyHashedPassword(null, user.Password, loginDTO.Password) == PasswordVerificationResult.Failed);
            if (user == null || verificationResult )
            {
                return false;
            }
            return true;
        }
    }
}
