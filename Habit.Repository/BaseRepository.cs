using Habit.Core;
using Habit.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            var res = await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var res = await _dbSet.ToListAsync();
            return res;
        }

        public async Task<T> GetByIdAsync(int id)
        {
           var res = await _dbSet.FindAsync(id);
           return  res;
        }

        public async Task UpdateAsync(T entity)
        {
          _dbSet.Update(entity); 
        }
    }
}
