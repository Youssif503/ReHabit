using System.ComponentModel.DataAnnotations.Schema;

namespace ReHabit.Habit.Core.Models
{
    public class HabitModel
    {
        public int Id { get; set; }
        public string HabitName { get; set; }
        public string Description { get; set; }
        public int? CurrentStreak { get; set; }
        public int? MaxStreak { get; set; }
        public DateOnly? LastCompletedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public ICollection<HabitEntry> Entries { get; set; } = new HashSet<HabitEntry>();
    }
}
