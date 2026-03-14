using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Deep_lines_Backend.BLL.Interfaces.IRepos
{
    public interface IGenericRepo<T> where T  : class
    {
        public Task<T>? GetByIdAsync(int id);
        public Task<List<T>> GetAllAsync();
        public Task<List<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
        public Task AddAsync(T entity);
        public void UpdateAsync(T entity);
        public Task? DeleteAsync(T entity);

    }
}
