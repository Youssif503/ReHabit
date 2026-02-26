using Microsoft.AspNetCore.Http;
namespace Habit.Core.ServiceContract
{
    public interface IImageService
    {
        Task<string?> SaveImageAsync(IFormFile image);
    }
}
