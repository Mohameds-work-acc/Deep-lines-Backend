using Deep_lines_Backend.BLL.DTOs.ReviewEntity;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IReviewService
    {
        public Task AddReview(AddReviewDTO reviewDTO);
        public Task<List<Review>> GetAll();
        public Task<Review> GetById(int id);
        public Task<bool> UpdateReview(AddReviewDTO reviewDTO, int id);
        public Task<bool> DeleteReview(int id);
    }
}
