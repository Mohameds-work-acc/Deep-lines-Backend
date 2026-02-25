using Deep_lines_Backend.BLL.Interfaces.IRepos;
using Deep_lines_Backend.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deep_lines_Backend.DAL.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {

        private readonly AppDbContext dbContext_;
        private readonly DbSet<T> dbset;

        public GenericRepo(AppDbContext dbContext)
        {
            this.dbContext_ = dbContext;
            this.dbset = dbContext_.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbset.AddAsync(entity);
            await dbContext_.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbset.Update(entity);
            await dbContext_.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbset.Remove(entity);
            await dbContext_.SaveChangesAsync();
        }
    }
}
