using Deep_lines_Backend.BLL.DTOs.BlogEntity;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using Deep_lines_Backend.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IEmployeeService
    {
        public Task AddUser(AddUserDTO userDTO);
        public Task<Employee> GetById(int id);
        public Task<Employee> GetByEmail(string email);
        public Task<List<Employee>> GetAll();
        public Task<bool> UpdateUser(AddUserDTO userDTO, int id);
        public Task<bool> DeleteUser(int id);
    }
}
