using System.ComponentModel.DataAnnotations.Schema;

namespace ReHabit.Habit.Core.Models
{
    public class HabitEntry
    {
        public int Id { get; set; }
        public DateOnly CompletedAt { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int HabitId { get; set; }
        [ForeignKey("HabitId")]
        public HabitModel? Habit { get; set; }
    }
}
