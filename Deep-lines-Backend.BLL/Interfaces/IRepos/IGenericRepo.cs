using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.BLL.Interfaces.IRepos
{
    public interface IGenericRepo<T> where T  : class
    {
        public Task<T>? GetByIdAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task? DeleteAsync(T entity);

    }
}
