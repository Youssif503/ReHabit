using ReHabit.Habit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Core.RepositoryContract
{
    public interface IHabitRepository:IBaseRepository<HabitModel>
    {
        // Add any additional methods specific to HabitModel if needed
    }
}
