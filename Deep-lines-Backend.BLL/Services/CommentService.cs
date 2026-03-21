using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.CommentEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<Comment> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateComment(AddCommentDTO commentDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(commentDTO, recorded);
            repo.UpdateAsync(recorded);
            return true;
        }

        public async Task<List<Comment>> GetByBlogId(int blogId)
        {
            var all = await repo.GetAllAsync();
            return all.Where(c => c.BlogId.HasValue && c.BlogId.Value == blogId).ToList();
        }
    }
}
