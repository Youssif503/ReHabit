using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Repository.Helpers
{
    public class ResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}
