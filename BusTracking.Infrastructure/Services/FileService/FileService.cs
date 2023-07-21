using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace BusTracking.Infrastructure.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment _hostEnvironment;

        public FileService(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        //public async Task<byte[]> GetFile(string folderName, string fileName)
        //{
        //    var filePath = Path.Combine(_hostEnvironment.WebRootPath, folderName, fileName);
        //    return await File.ReadAllBytesAsync(filePath);
        //}
        public async Task<byte[]> GetFile(string folderName, string fileName)
        {
            var webRootPath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot");
            var filePath = Path.Combine(webRootPath, folderName, fileName);
            return await File.ReadAllBytesAsync(filePath);
        }


        public async Task<string> GetFileBase64(string folderName, string fileName)
        {
            var file = await GetFile(folderName, fileName);
            return Convert.ToBase64String(file);
        }

        public async Task<string> SaveFile(IFormFile file, string folderName)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var contentRootPath = _hostEnvironment.ContentRootPath;
                var webRootPath = Path.Combine(contentRootPath, "wwwroot");
                var uploads = Path.Combine(webRootPath, folderName);

                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                await using var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }


        public async Task<string> SaveFile(byte[] file, string folderName, string extension)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var contentRootPath = _hostEnvironment.ContentRootPath;
                var webRootPath = Path.Combine(contentRootPath, "wwwroot");
                var uploads = Path.Combine(webRootPath, folderName);

                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), file);
            }

            return fileName;
        }


        public async Task<string> SaveFile(string file, string folderName, string extension)
        {
            string fileName = null;
            if (!string.IsNullOrWhiteSpace(file))
            {
                file = file.Substring(file.IndexOf(",", StringComparison.Ordinal) + 1);
                var bytes = Convert.FromBase64String(file);

                var contentRootPath = _hostEnvironment.ContentRootPath;
                var webRootPath = Path.Combine(contentRootPath, "wwwroot");
                var uploads = Path.Combine(webRootPath, folderName);

                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), bytes);
            }

            return fileName;
        }

    }
}
