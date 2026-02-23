using Deep_lines_Backend.BLL.DTOs.CommentEntity;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface ICommentService
    {
        public Task AddComment(AddCommentDTO commentDTO);
        public Task<List<Comment>> GetAll();
        public Task<Comment> GetById(int id);
        public Task<bool> UpdateComment(AddCommentDTO commentDTO, int id);
        public Task<bool> DeleteComment(int id);
    }
}
