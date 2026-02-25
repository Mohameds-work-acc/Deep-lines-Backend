using Azure.Identity;
using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IRepos
{
    public interface IRefreshTokenRepo
    {
        public RefreshTokenDTO? CheckRefreshToken(int userID , string token);
        public void addRefreshToken(RefreshToken refreshToken);
        public void removeRefreshToken(int tokenID);
    }
}
