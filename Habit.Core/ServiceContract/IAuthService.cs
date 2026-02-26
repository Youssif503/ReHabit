using ReHabit.Habit.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Core.ServiceContract
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(User user);
    }
}
