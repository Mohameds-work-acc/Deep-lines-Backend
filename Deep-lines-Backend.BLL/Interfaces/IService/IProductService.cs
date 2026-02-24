using Deep_lines_Backend.BLL.DTOs.ProductEntity;
using Deep_lines_Backend.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deep_lines_Backend.BLL.Interfaces.IService
{
    public interface IProductService
    {
        public Task AddProduct(AddProductDTO productDTO);
        public Task<List<Product>> GetAll();
        public Task<Product> GetById(int id);
        public Task<bool> UpdateProduct(AddProductDTO productDTO, int id);
        public Task<bool> DeleteProduct(int id);
    }
}
