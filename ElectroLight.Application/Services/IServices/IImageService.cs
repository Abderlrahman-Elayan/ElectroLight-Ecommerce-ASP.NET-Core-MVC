using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectroLight.Application.Services.IServices
{
    public interface IImageService
    {
        public  Task<string> UploadImageAsync(IFormFile Image, string OldImgaeUrl);
        public bool DeleteImage(string? ImageUrl);
        public string GetImageFullPath(string imageUrl);

    }
}
