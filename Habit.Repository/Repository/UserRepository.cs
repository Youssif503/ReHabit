using Habit.Core.RepositoryContract;
using Habit.Repository.Data;
using ReHabit.Habit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Repository.Repository
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }
        // You can add any specific methods for User here if needed
    }
}
