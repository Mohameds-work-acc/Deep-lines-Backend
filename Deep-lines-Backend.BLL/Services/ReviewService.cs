using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.ReviewEntity;
using Deep_lines_Backend.BLL.Interfaces;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepo<Review> repo;
        private readonly IGenericRepo<Product> productRepo;
        private readonly IMapper mapper;

        public ReviewService(IGenericRepo<Review> repo, IGenericRepo<Product> productRepo, IMapper mapper)
        {
            this.repo = repo;
            this.productRepo = productRepo;
            this.mapper = mapper;
        }

        public async Task AddReview(AddReviewDTO reviewDTO)
        {
            var mapped = mapper.Map<Review>(reviewDTO);
            if (reviewDTO.Product_Id != 0)
            {
                mapped.product = await productRepo.GetByIdAsync(reviewDTO.Product_Id);
            }
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteReview(int id)
        {
            var review = await repo.GetByIdAsync(id);
            if (review == null) return false;
            await repo.DeleteAsync(review);
            return true;
        }

        public Task<List<Review>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<Review> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateReview(AddReviewDTO reviewDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(reviewDTO, recorded);
            if (reviewDTO.Product_Id != 0)
            {
                recorded.product = await productRepo.GetByIdAsync(reviewDTO.Product_Id);
            }
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
