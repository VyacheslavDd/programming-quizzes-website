using Core.Enums;
using Core.Redis.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Redis.Implementations
{
	public class RedisService : IRedisService
	{
		private readonly IDistributedCache _distributedCache;

		public RedisService(IDistributedCache distributedCache)
		{
			_distributedCache = distributedCache;
		}

		public async Task<Tuple<T, RedisServiceResponse>> Get<T>(string key)
		{
			var stringValue = "";
			RedisServiceResponse response; 
			try
			{
				stringValue = await _distributedCache.GetStringAsync(key);
				response = RedisServiceResponse.Success;
			}
	        catch (RedisConnectionException)
			{
				response = RedisServiceResponse.Failure;
			}
			if (string.IsNullOrEmpty(stringValue))
			{
				return Tuple.Create<T, RedisServiceResponse>(default, response);
			}
			var result = JsonConvert.DeserializeObject<T>(stringValue);
			return Tuple.Create(result, response);
		}

		public async Task Set<T>(string key, T value)
		{
			try
			{
				var serializedObject = JsonConvert.SerializeObject(value, new JsonSerializerSettings
				{

					ReferenceLoopHandling = ReferenceLoopHandling.Ignore
				});
				await _distributedCache.SetStringAsync(key, serializedObject);
			}
			catch (RedisConnectionException)
			{

			}
		}
	}
}
