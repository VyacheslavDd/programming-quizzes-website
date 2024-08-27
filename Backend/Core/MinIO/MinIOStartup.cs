using Core.Base.Service.Implementations;
using Core.Base.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MinIO
{
	public static class MinIOStartup
	{
		public static IServiceCollection AddMinIOService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IImageService, ImageService>();
			var endpoint = configuration.GetSection("MinIO:Endpoint").Value;
			var accessKey = configuration.GetSection("MinIO:AccessKey").Value;
			var secretKey = configuration.GetSection("MinIO:SecretKey").Value;
			services.AddMinio(config => config.WithEndpoint(endpoint).WithCredentials(accessKey, secretKey));
			return services;
		}
	}
}
