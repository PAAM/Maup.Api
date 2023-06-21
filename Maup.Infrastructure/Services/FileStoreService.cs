using Maup.Core.Entities;
using Maup.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace Maup.Infrastructure.Services
{
    public class FileStoreService : IFileStore
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        public FileStoreService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> SaveFile(IFormFile formFile)
        {
            using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            var fileBytes = stream.ToArray();
            var fileStore = new FileStore()
            {
                File = fileBytes,
                FileName = Guid.NewGuid().ToString(),
                FileType = Path.GetExtension(formFile.FileName),
                Container = "Images",
                ContentType = formFile.ContentType
            };
            var path = Create(fileStore);
            return path;
        }

        private string Create(FileStore fileStore)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(webRootPath))
            {
                throw new Exception("WebRootPath is null or Empty");
            }

            string fileFolder = Path.Combine(webRootPath, fileStore.Container);
            if (!Directory.Exists(fileFolder))
            {
                Directory.CreateDirectory(fileFolder);
            }

            string fileName = $"{fileStore.FileName}{fileStore.FileType}";
            string finalPath = Path.Combine(fileFolder, fileName);
            File.WriteAllBytesAsync(finalPath, fileStore.File);
            string currentUrl = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}";
            string imgUrl = Path.Combine(currentUrl, fileStore.Container, fileName).Replace("\\","/");
            return imgUrl;
        }
    }
}
