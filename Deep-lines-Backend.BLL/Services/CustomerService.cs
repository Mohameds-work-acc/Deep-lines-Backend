using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.CustomerEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepo<Customer> repo;
        private readonly IMapper mapper;

        public CustomerService(IGenericRepo<Customer> repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task AddCustomer(AddCustomerDTO customerDTO)
        {
            var mapped = mapper.Map<Customer>(customerDTO);
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await repo.GetByIdAsync(id);
            if (customer == null) return false;
            await repo.DeleteAsync(customer);
            return true;
        }

        public Task<List<Customer>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateCustomer(AddCustomerDTO customerDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(customerDTO, recorded);
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
