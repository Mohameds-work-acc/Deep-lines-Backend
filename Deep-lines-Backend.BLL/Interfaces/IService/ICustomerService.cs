using Deep_lines_Backend.BLL.DTOs.CustomerEntity;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface ICustomerService
    {
        public Task AddCustomer(AddCustomerDTO customerDTO);
        public Task<List<Customer>> GetAll();
        public Task<Customer> GetById(int id);
        public Task<bool> UpdateCustomer(AddCustomerDTO customerDTO, int id);
        public Task<bool> DeleteCustomer(int id);
    }
}
