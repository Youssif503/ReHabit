using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReHabit.Habit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace Habit.Repository.Data
{
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        public DbSet<HabitModel> Habits { get; set; }
        public DbSet<HabitEntry> HabitEntries { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }
    }
}
