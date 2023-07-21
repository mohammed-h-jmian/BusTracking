using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.FileService
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string folderName);
        Task<string> SaveFile(string file, string folderName, string extension);
        Task<string> SaveFile(byte[] file, string folderName, string extension);
        Task<byte[]> GetFile(string folderName, string fileName);
        Task<string> GetFileBase64(string folderName, string fileName);
    }
}
