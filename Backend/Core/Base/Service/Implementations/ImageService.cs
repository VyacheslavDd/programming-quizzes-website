
using Core.Base.Service.Interfaces;
using Core.Constants;
using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base.Service.Implementations
{
    public class ImageService : IImageService
    {
        private readonly ILogger _logger;

        public ImageService(ILogger logger)
        {
            _logger = logger;
        }

        public string CreateName(string fileName)
        {
            var uniqueName = Guid.NewGuid().ToString() + "_" + fileName;
            return uniqueName;
        }

        public async Task SaveFileAsync(IFormFile file, IMinioClient minioClient, string bucketName, string fileName)
        {
            var doesBucketExist = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (!doesBucketExist) await minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                var objectArgs = new PutObjectArgs().WithBucket(bucketName).WithObject(file.FileName)
                    .WithContentType(file.ContentType).WithStreamData(stream).WithObjectSize(stream.Length);
                await minioClient.PutObjectAsync(objectArgs);
			}
        }

        public async Task DeleteFile(IMinioClient minioClient, string bucketName, string fileName)
        {
			var doesBucketExist = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (doesBucketExist)
            {
                try
                {
                    var removeObjectArgs = new RemoveObjectArgs().WithBucket(bucketName).WithObject(fileName);
                    await minioClient.RemoveObjectAsync(removeObjectArgs);
                }
                catch
                {
                    _logger.Error("Не удалось удалить объект {fileName} из корзины {bucketName}", fileName, bucketName);
                }
            };
        }

        public async Task<byte[]> GetFileAsByteArrayAsync(IMinioClient minioClient, string bucketName, string fileName)
        {
			var doesBucketExist = await minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
            if (doesBucketExist)
            {
                using (var memoryStream = new MemoryStream()) {
                    try
                    {
                        var getObjectArgs = new GetObjectArgs().WithBucket(bucketName).WithObject(fileName)
                            .WithCallbackStream(stream => stream.CopyTo(memoryStream));
                        var obj = await minioClient.GetObjectAsync(getObjectArgs);
                        return memoryStream.ToArray();
                    }
                    catch
                    {
                        return Array.Empty<byte>();
                    }
                }
            }
            return Array.Empty<byte>();
        }
    }
}
