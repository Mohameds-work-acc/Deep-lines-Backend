using Deep_lines_Backend.BLL.DTOs.AuthServices;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.DAL.Context;
using Deep_lines_Backend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Repositories
{
    public class RefreshTokenRepo : IRefreshTokenRepo
    {
        private readonly AppDbContext dbContext;

        public RefreshTokenRepo(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void addRefreshToken(RefreshToken refreshToken)
        {
            dbContext.RefreshTokens.Add(refreshToken);
            
            dbContext.SaveChanges();
        }

        public TokensResponse? CheckRefreshToken(int userID, string token)
        {
            var tokenRecorded = dbContext.RefreshTokens.FirstOrDefault(r=> r.Token == token);
            if (tokenRecorded == null)
            {
                return null;
            }

            if(tokenRecorded.UserId != userID)
            {
                return null;
            }

            if (!tokenRecorded.IsActive)
            {
                return null;
            }
            return new TokensResponse
            {
                RefreshToken = tokenRecorded.Token,
                RefreshTokenExpires = tokenRecorded.Expiration
            };
        }

        public List<RefreshToken>? getRefreshTokenList(int userId)
        {
            var refreshTokens = dbContext.RefreshTokens.Where(r=> r.UserId == userId).ToList();
            return refreshTokens;
        }

        public void removeRefreshToken(string token)
        {
            var recordedToken = dbContext.RefreshTokens.FirstOrDefault(r => r.Token == token);
            if (recordedToken != null)
            {
                dbContext.RefreshTokens.Remove(recordedToken);
                dbContext.SaveChanges();
            }
        }
    }
}
