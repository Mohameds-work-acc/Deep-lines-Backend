using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepo<Employee> repo;
        private readonly IMapper mapper;
        private readonly PasswordHasher<object> passwordHasher = new();

        public EmployeeService(IGenericRepo<Employee> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task AddUser(AddUserDTO userDTO)
        {
            var mapped = mapper.Map<Employee>(userDTO);

            mapped.Password = passwordHasher.HashPassword(null, mapped.Password);

            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await repo.GetByIdAsync(id);
            if (user == null) return false;
            await repo.DeleteAsync(user);
            return true;
        }

        public Task<List<Employee>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public Task<Employee> GetByEmail(string email)
        {
            
            var users = repo.GetAllAsync().Result;

            return Task.FromResult(users.FirstOrDefault(u => u.Email == email));

        }

        public async Task<Employee> GetById(int id)
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
