using ElectroLight.Application.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroLight.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<string> UploadImageAsync(IFormFile Image, string OldImgaeUrl)
        {
            var ext = Path.GetExtension(Image.FileName).ToLower();
            if (!allowedExtensions.Contains(ext))
            {
                throw new ArgumentException("Only image files are allowed.");
            }

            DeleteImage(OldImgaeUrl);

            string fileName = $"{Guid.NewGuid()}{ext}";

            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "ProductImages");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }

            return $"/img/ProductImages/{fileName}";
        }

        public bool DeleteImage(string ImageUrl)
        {

            if (!string.IsNullOrEmpty(ImageUrl)
                && ImageUrl != "/img/placeholder.jpg")
            {
                string oldImagePath = GetImageFullPath(ImageUrl);

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                    return true;
                }
            }
            return false;
        }

        public string GetImageFullPath(string imageUrl)
        {
            string ImageFullPath =
                     Path.Combine(_webHostEnvironment.WebRootPath, (imageUrl ?? string.Empty)
                     .TrimStart('/', '\\')
                     .Replace('/', Path.DirectorySeparatorChar)
                     .Replace('\\', Path.DirectorySeparatorChar));
            return ImageFullPath;

        }
    }
}
