using Microsoft.AspNetCore.Identity;
namespace ReHabit.Habit.Core.Models
{
    public class User:IdentityUser
    {
        public string ImageUrl { get; set; }
        public List<HabitModel> Habits { get; set; }
    }
}
