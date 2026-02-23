using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using Deep_lines_Backend.BLL.Interfaces;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepo<User> repo;
        private readonly IMapper mapper;

        public UserService(IGenericRepo<User> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task AddUser(AddUserDTO userDTO)
        {
            var mapped = mapper.Map<User>(userDTO);
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null) return false;
            await repo.DeleteAsync(user);
            return true;
        }

        public Task<List<User>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateUser(AddUserDTO userDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(userDTO, recorded);
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
