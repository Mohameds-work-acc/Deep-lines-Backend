using AutoMapper;
using Deep_lines_Backend.BLL.DTOs.OrderEntity;
using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepo<Order> repo;
        private readonly IGenericRepo<Customer> customerRepo;
        private readonly IMapper mapper;

        public OrderService(IGenericRepo<Order> repo, IGenericRepo<Customer> customerRepo, IMapper mapper)
        {
            this.repo = repo;
            this.customerRepo = customerRepo;
            this.mapper = mapper;
        }

        public async Task AddOrder(AddOrderDTO orderDTO)
        {
            var mapped = mapper.Map<Order>(orderDTO);
            // resolve customer if provided
            if (orderDTO.Customer_Id != 0)
            {
                var customer = await customerRepo.GetByIdAsync(orderDTO.Customer_Id);
                mapped.Customer = customer;
            }
            await repo.AddAsync(mapped);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await repo.GetByIdAsync(id);
            if (order == null) return false;
            await repo.DeleteAsync(order);
            return true;
        }

        public Task<List<Order>> GetAll()
        {
            return repo.GetAllAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await repo.GetByIdAsync(id);
        }

        public async Task<bool> UpdateOrder(AddOrderDTO orderDTO, int id)
        {
            var recorded = await repo.GetByIdAsync(id);
            if (recorded == null) return false;
            mapper.Map(orderDTO, recorded);
            if (orderDTO.Customer_Id != 0)
            {
                var customer = await customerRepo.GetByIdAsync(orderDTO.Customer_Id);
                recorded.Customer = customer;
            }
            await repo.UpdateAsync(recorded);
            return true;
        }
    }
}
