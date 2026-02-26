using Habit.Core.ServiceContract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace Habit.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string?> SaveImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return null;
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + uniqueFileName;
        
        }
    }
}
