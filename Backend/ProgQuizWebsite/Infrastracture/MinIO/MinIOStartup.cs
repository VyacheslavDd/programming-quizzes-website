using Minio;

namespace ProgQuizWebsite.Infrastracture.MinIO
{
	public static class MinIOStartup
	{
		public static IServiceCollection AddMinIOService(this IServiceCollection services, IConfiguration configuration)
		{
			var endpoint = configuration.GetSection("MinIO:Endpoint").Value;
			var accessKey = configuration.GetSection("MinIO:AccessKey").Value;
			var secretKey = configuration.GetSection("MinIO:SecretKey").Value;
			services.AddMinio(config => config.WithEndpoint(endpoint).WithCredentials(accessKey, secretKey));
			return services;
		}
	}
}
