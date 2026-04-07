using ElectroLight.Application.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Uploads an image and normalizes its size (crop to fixed square for sliders).
        /// </summary>
        /// <param name="imageFile">Uploaded image file</param>
        /// <param name="oldImageUrl">Optional old image to delete</param>
        /// <param name="imagesFolderName">Folder under wwwroot/img</param>
        /// <param name="width">Width of output image (default 600)</param>
        /// <param name="height">Height of output image (default 600)</param>
        /// <returns>Relative path to saved image</returns>
        /// 
        public async Task<string> UploadAndNormalizeImageAsync(
        IFormFile imageFile,
        string oldImageUrl,
        string imagesFolderName,
        int width = 600,
        int height = 600)
        {
            if (imageFile == null)
                throw new ArgumentNullException(nameof(imageFile));

            var ext = Path.GetExtension(imageFile.FileName).ToLower();
            if (!allowedExtensions.Contains(ext))
                throw new ArgumentException("Only image files are allowed.");

            DeleteImage(oldImageUrl);

            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", imagesFolderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);


            string fileName = $"{Guid.NewGuid():N}.webp";
            string fullPath = Path.Combine(folderPath, fileName);

            using var image = await Image.LoadAsync(imageFile.OpenReadStream());

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Crop,
                Position = AnchorPositionMode.Center
            }));

            await image.SaveAsWebpAsync(fullPath, new WebpEncoder
            {
                Quality = 85
            });

            return $"/img/{imagesFolderName}/{fileName}";
        }



        //public async Task<string> UploadAndNormalizeImageAsync(IFormFile imageFile, string oldImageUrl, string imagesFolderName, int width = 600, int height = 600)
        //{
        //    if (imageFile == null)
        //        throw new ArgumentNullException(nameof(imageFile));

        //    var ext = Path.GetExtension(imageFile.FileName).ToLower();
        //    if (!allowedExtensions.Contains(ext))
        //        throw new ArgumentException("Only image files are allowed.");

        //    // Delete old image if exists
        //    DeleteImage(oldImageUrl);

        //    // Ensure folder exists
        //    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", imagesFolderName);
        //    if (!Directory.Exists(folderPath))
        //    {
        //        Directory.CreateDirectory(folderPath);
        //    }

        //    string fileName = $"{Guid.NewGuid():N}{ext}";
        //    string fullPath = Path.Combine(folderPath, fileName);

        //    // Load image and normalize
        //    using var image = await Image.LoadAsync(imageFile.OpenReadStream());

        //    // Crop or pad image to fixed size for uniform slider display
        //    image.Mutate(x => x.Resize(new ResizeOptions
        //    {
        //        Size = new Size(width, height),
        //        Mode = ResizeMode.Crop, // Use Crop to fill the container
        //        Position = AnchorPositionMode.Center
        //    }));


        //    await image.SaveAsJpegAsync(fullPath, new JpegEncoder { Quality = 100 });

        //    return $"/img/{imagesFolderName}/{fileName}";
        //}

        //public async Task<string> UploadImageAsync(IFormFile Image, string OldImgaeUrl,string ImagesFolderName)
        //{
        //    var ext = Path.GetExtension(Image.FileName).ToLower();
        //    if (!allowedExtensions.Contains(ext))
        //    {
        //        throw new ArgumentException("Only image files are allowed.");
        //    }

        //    DeleteImage(OldImgaeUrl);

        //    string fileName = $"{Guid.NewGuid()}{ext}";

        //    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", ImagesFolderName);

        //    if (!Directory.Exists(folderPath))
        //    {
        //        Directory.CreateDirectory(folderPath);
        //    }

        //    string fullPath = Path.Combine(folderPath, fileName);

        //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //    {
        //        await Image.CopyToAsync(fileStream);
        //    }


        //    return $"/img/{ImagesFolderName}/{fileName}";
        //}

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
