using Deep_lines_Backend.BLL.DTOs.OrderEntity;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IOrderService
    {
        public Task AddOrder(AddOrderDTO orderDTO);
        public Task<List<Order>> GetAll();
        public Task<Order> GetById(int id);
        public Task<bool> UpdateOrder(AddOrderDTO orderDTO, int id);
        public Task<bool> DeleteOrder(int id);
    }
}
