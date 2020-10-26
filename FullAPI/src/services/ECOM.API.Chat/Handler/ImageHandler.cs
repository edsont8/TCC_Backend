﻿using ECOM.WebChat.AvatarWriter.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECOM.API.Chat.Handler
{
    public interface IImageHandler
    {
        Task<IActionResult> UploadImage(IFormFile file);
    }

    public class ImageHandler : IImageHandler
    {
        private readonly IAvatarWriter _imageWriter;
        public ImageHandler(IAvatarWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _imageWriter.UploadImage(file);
            return new ObjectResult(result);
        }
    }
}
