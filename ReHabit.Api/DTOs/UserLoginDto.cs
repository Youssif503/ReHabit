using System.ComponentModel.DataAnnotations;

namespace ReHabit.Api.DTOs
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
