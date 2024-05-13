using Core.Redis.Implementations;
using Core.Redis.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Redis
{
	public static class RedisStartup
	{
		public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = configuration.GetConnectionString("Redis");
			});
			services.AddScoped<IRedisService, RedisService>();
			return services;
		}
	}
}
