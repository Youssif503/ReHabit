using Habit.Core;
using Habit.Core.RepositoryContract;
using Habit.Repository.Data;
using Habit.Repository.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Habits = new HabitRepository(_context);
            Users = new UserRepository(_context);
        }
        public IHabitRepository Habits { get;private set; }
        public IUserRepository Users { get; private set; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                _context.SaveChanges();
                _transaction.Commit();
            }
            catch(Exception ex)
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        public async Task RollbackTransactionAsync()
        {
           if(_transaction != null)
           {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
           }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
