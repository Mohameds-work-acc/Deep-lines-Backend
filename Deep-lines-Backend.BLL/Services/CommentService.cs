using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.CommentEntity;
using Deep_lines_Backend.BLL.Interfaces;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepo<Comment> repo;
        private readonly IMapper mapper;

        public CommentService(IGenericRepo<Comment> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task AddComment(AddCommentDTO commentDTO)
        {
            var mapped = mapper.Map<Comment>(commentDTO);
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteComment(int id)
        {
            var comment = await repo.GetByIdAsync(id);
            if (comment == null) return false;
            await repo.DeleteAsync(comment);
            return true;
        }

        public Task<List<Comment>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateComment(AddCommentDTO commentDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(commentDTO, recorded);
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
