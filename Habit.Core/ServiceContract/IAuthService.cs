using System;
using System.Collections.Generic;
using System.Text;

namespace Habit.Core.ServiceContract
{
    public interface IAuthService
    {
        Task<string> CreateTokenAsync(User user)
        {
                       // Implementation for creating a token based on the user information
            // This is just a placeholder and should be replaced with actual token generation logic
            return Task.FromResult("GeneratedTokenForUser");
        }
    }
}
