using Microsoft.AspNetCore.Http;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Service.Interfaces
{
    public interface IImageService
    {
        public string CreateName(string fileName);
        public Task SaveFileAsync(IFormFile file, IMinioClient minioClient, string bucketName, string fileName);
        public Task DeleteFile(IMinioClient minioClient, string bucketName, string fileName);
        public Task<byte[]> GetFileAsByteArrayAsync(IMinioClient minioClient, string bucketName, string fileName);
    }
}
