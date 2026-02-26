using Habit.Core.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Core
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        public IHabitRepository Habits { get;}

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
