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

        public RefreshTokenDTO? CheckRefreshToken(int userID, string token)
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
            return new RefreshTokenDTO
            {
                Id = tokenRecorded.Id,
                Token = tokenRecorded.Token,
                ExpiryDate = tokenRecorded.Expiration
            };
        }

        public void removeRefreshToken(int tokenID)
        {
            var recordedToken = dbContext.RefreshTokens.FirstOrDefault(r => r.Id == tokenID);
            if (recordedToken != null)
            {
                dbContext.RefreshTokens.Remove(recordedToken);
                dbContext.SaveChanges();
            }
        }
    }
}
