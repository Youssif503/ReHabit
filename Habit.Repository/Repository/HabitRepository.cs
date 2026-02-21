using Habit.Core.RepositoryContract;
using Habit.Repository.Data;
using ReHabit.Habit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Repository.Repository
{
    public class HabitRepository:BaseRepository<HabitModel>, IHabitRepository
    {
        private readonly ApplicationDbContext _context;
        public HabitRepository(ApplicationDbContext context)
            :base(context)
        {
            _context = context;
        }
        // You can add any specific methods for HabitModel here if needed
    }
}
